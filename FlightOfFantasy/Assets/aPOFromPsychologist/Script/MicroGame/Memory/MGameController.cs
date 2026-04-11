using System;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class MGameController : GameController
    {
        [SerializeField] private Button btnHideAllCard;
        [SerializeField] private Button button4;
        [SerializeField] private Button button6;
        [SerializeField] private Button button8;

        [SerializeField] private Button btnNextRound;
        [SerializeField] private Button btnNewDiffecalty;
        [SerializeField] private Button btnExitMenu;
        [SerializeField] private GameObject panelSelectDiffecalty;
        [SerializeField] private GameObject panelEndGame;
        [SerializeField] private MGeneratedLevel generatedLevel;

        public Action EndGameAction;
        [Inject] private EntryPoint entryPoint;
        private int diffecaltyGame;

        void Start()
        {
            panelSelectDiffecalty.SetActive(true);            
        }

        private void OnEnable()
        {
            button4.onClick.AddListener(() => { StartGenerate(8); });
            button6.onClick.AddListener(() => { StartGenerate(12); });
            button8.onClick.AddListener(() => { StartGenerate(16); });
            btnNextRound.onClick.AddListener(NextRound);
            btnNewDiffecalty.onClick.AddListener(NewDiffecalty);
            btnExitMenu.onClick.AddListener(() => { entryPoint.LoadScene(1); });
            btnHideAllCard.onClick.AddListener(HideAllCard);
            EndGameAction += EndGame;
        }

        private void OnDisable()
        {
            button4.onClick.RemoveListener(() => { StartGenerate(8); });
            button6.onClick.RemoveListener(() => { StartGenerate(12); });
            button8.onClick.RemoveListener(() => { StartGenerate(16); });
            btnNextRound.onClick.RemoveListener(NextRound);
            btnNewDiffecalty.onClick.RemoveListener(NewDiffecalty);
            btnExitMenu.onClick.RemoveListener(() => { entryPoint.LoadScene(1); });
            btnHideAllCard.onClick.RemoveListener(HideAllCard);
            EndGameAction -= EndGame;
        }

        private void StartGenerate(int size)
        {
            diffecaltyGame = size;
            panelSelectDiffecalty.SetActive(false);
            generatedLevel.GenerateLevel(size);
            MCardManager.Instance.ShowAllCardAndTurnOffInteractible();
            btnHideAllCard.interactable = true;
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
