using UnityEngine;
using System;
using UnityEngine.UI;

namespace DiplomGames
{
    public class FSAGameController : GameController
    {
        [SerializeField] private FSASlotManager slotManager;
        [SerializeField] private FSAChecketSlot checkerSlot;
        [SerializeField] private FSAUiView uiView;
        [SerializeField] private SliderLevelComplexity sliderLevelComplexity;
        [SerializeField] private GameObject panelStart;
        [SerializeField] private GameObject panelEnd;

        [Header("Кнопки")]
        [SerializeField] private Button btnNextGame;
        [SerializeField] private Button btnChangeDiffecalty;
        [SerializeField] private Button btnGoToMenu;

        [Inject] private EntryPoint entryPoint;
        [Inject] private PlayPhrasesVetricksOnCall playPhrase;

        public Action StartNextGame;

        private (Sprite, Transform) currentGame;
        private LevelComplexity levelSettings;
        private int currentRound = 0;
        private bool isDontFirstGame;

        private void OnEnable()
        {
            StartNextGame += NextRound;
            sliderLevelComplexity.AcceptComplexityChanged += StartGame;

            btnNextGame.onClick.AddListener(() => StartGame(levelSettings));
            btnChangeDiffecalty.onClick.AddListener(NewDiffecalty);
            btnGoToMenu.onClick.AddListener(() => { entryPoint.LoadScene(1); });
        }

        private void OnDisable()
        {
            StartNextGame -= NextRound;
            sliderLevelComplexity.AcceptComplexityChanged -= StartGame;

            btnNextGame.onClick.RemoveListener(StartGame);
            btnChangeDiffecalty.onClick.RemoveListener(NewDiffecalty);
            btnGoToMenu.onClick.RemoveListener(() => { entryPoint.LoadScene(1); });
        }


        private void StartGame(LevelComplexity levelComplexity)
        {
            panelEnd.SetActive(false);
            panelStart.SetActive(false);
            levelSettings = levelComplexity;
            currentRound = 0;

            currentGame = slotManager.StartGame();
            uiView.UpdateSpriteProp(currentGame.Item1);
            checkerSlot.UpdateRightQuestion(currentGame.Item2);


            if (isDontFirstGame == false)
            {
                playPhrase.PlayWelcomePhrase();
                isDontFirstGame = true;
            }
        }

        protected override async void NextRound()
        {
            currentRound++;

            await slotManager.NextGame();
            if (!levelSettings.Infinity && currentRound >= levelSettings.CurrentLevelComplexity)
            {
                EndGame();
                return;
            }
            await slotManager.SetScaleToZero();

            currentGame = slotManager.GeneratedNewLevel();
            uiView.UpdateSpriteProp(currentGame.Item1);
            await slotManager.SetScaleToBase();
            checkerSlot.UpdateRightQuestion(currentGame.Item2);
        }

        protected override void EndGame()
        {
            panelEnd.SetActive(true); 
        }

        public void NewDiffecalty()
        {
            panelStart.SetActive(true);
            panelEnd.SetActive(false);
        }
    }
}
