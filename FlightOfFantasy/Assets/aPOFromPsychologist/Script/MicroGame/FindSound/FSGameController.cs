using System;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class FSGameController : GameController
    {
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private FSCheckerSlot checkerSlot; 
        [SerializeField] private UiViewFS uiViewFS;
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

        private FSSoundList currentGame;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                currentRound++;
        }


        private async void StartGame(LevelComplexity levelComplexity)
        {
            panelEnd.SetActive(false);
            panelStart.SetActive(false);
            levelSettings = levelComplexity;
            currentRound = 0;

            currentGame = await slotManager.StartGame();
            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);

            if (isDontFirstGame == false)
            {
                playPhrase.PlayWelcomePhrase();
                isDontFirstGame = true;
            }
        }

        protected override async void NextRound()
        {
            currentRound++;

            currentGame = await slotManager.NextGame();
            if (!levelSettings.Infinity && currentRound >= levelSettings.CurrentLevelComplexity)
            {
                EndGame();
                return;
            }

            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);
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
