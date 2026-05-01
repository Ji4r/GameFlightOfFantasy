using System;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class MGameController : GameController
    {
        [SerializeField] private Button btnHideAllCard;
        [SerializeField] private SliderLevelComplexity sliderLevelComplexity;

        [SerializeField] private Button btnNextRound;
        [SerializeField] private Button btnNewDiffecalty;
        [SerializeField] private Button btnExitMenu;
        [SerializeField] private GameObject panelSelectDiffecalty;
        [SerializeField] private GameObject panelEndGame;
        [SerializeField] private MGeneratedLevel generatedLevel;

        [Inject] private EntryPoint entryPoint;
        [Inject] private PlayPhrasesVetricksOnCall playPhrase;

        public Action EndGameAction;


        private LevelComplexity diffecaltyGame;
        private bool isDontFirstGame;
        void Start()
        {
            panelSelectDiffecalty.SetActive(true);            
        }

        private void OnEnable()
        {
            sliderLevelComplexity.AcceptComplexityChanged += StartGenerate;
            btnNextRound.onClick.AddListener(NextRound);
            btnNewDiffecalty.onClick.AddListener(NewDiffecalty);
            btnExitMenu.onClick.AddListener(() => { entryPoint.LoadScene(1); });
            btnHideAllCard.onClick.AddListener(HideAllCard);
            EndGameAction += EndGame;
        }

        private void OnDisable()
        {
            sliderLevelComplexity.AcceptComplexityChanged -= StartGenerate;
            btnNextRound.onClick.RemoveListener(NextRound);
            btnNewDiffecalty.onClick.RemoveListener(NewDiffecalty);
            btnExitMenu.onClick.RemoveListener(() => { entryPoint.LoadScene(1); });
            btnHideAllCard.onClick.RemoveListener(HideAllCard);
            EndGameAction -= EndGame;
        }

        public void StartGenerate(LevelComplexity size)
        {
            diffecaltyGame = size;
            panelSelectDiffecalty.SetActive(false);
            generatedLevel.GenerateLevel(size.CurrentLevelComplexity);
            MCardManager.Instance.ShowAllCardAndTurnOffInteractible();
            btnHideAllCard.interactable = true;

            if (isDontFirstGame == false)
            {
                playPhrase.PlayWelcomePhrase();
                isDontFirstGame = true;
            }
        }

        protected override void NextRound()
        {
            panelEndGame.SetActive(false);
            StartGenerate(diffecaltyGame);
        }

        private void NewDiffecalty()
        {
            panelEndGame.SetActive(false);
            panelSelectDiffecalty.SetActive(true);
        }

        protected override void EndGame()
        {
            SoundPlayer.instance.PlaySound(ListSound.AllAnswerCorrectInMemory);
            panelEndGame.SetActive(true);
        }

        private void HideAllCard()
        {
            MCardManager.Instance.HideAllCardAndTurnOnInteractible();
            btnHideAllCard.interactable = false;
        }
    }
}
