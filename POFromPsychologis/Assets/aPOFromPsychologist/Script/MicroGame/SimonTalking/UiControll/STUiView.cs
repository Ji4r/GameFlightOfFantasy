using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STUiView : MonoBehaviour
    {
        [Header("Ссылки на элементы")]
        [SerializeField] private GameObject windowRestratGame;
        [SerializeField] private TextMeshProUGUI txtIsWin;

        [Header("Ссылки на кнопки")]
        [SerializeField] private Button buttonReplay;
        [SerializeField] private Button buttonNextRoundOnWindow;
        [SerializeField] private Button buttonStartGame;
        [SerializeField] private Button buttonPlaySequence;

        [Inject] private STBuilderGame builderGame;
        [Inject] private STColorValidator colorValidator;
        [Inject] private STGameController gameController;
        [Inject] private STGameSettingsManager gameSettingsManager;
        [Inject] private STHistoryColor historyColor;

        private bool isInitialized;

        private void OnEnable()
        {
            if (!isInitialized) return;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            buttonReplay.onClick.AddListener(OnRestartClick);
            buttonNextRoundOnWindow.onClick.AddListener(OnNextClick);
            colorValidator.AnErrorWasMade += ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect += EverythingIsCorrect;
            buttonStartGame.onClick.AddListener(StartGame);
            buttonPlaySequence.onClick.AddListener(NextRound);
        }

        private void OnDisable()
        {
            if (!isInitialized) return;

            buttonReplay.onClick.RemoveListener(OnRestartClick);
            buttonNextRoundOnWindow.onClick.RemoveListener(OnNextClick);
            colorValidator.AnErrorWasMade -= ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect -= EverythingIsCorrect;
            buttonStartGame.onClick.RemoveListener(StartGame);
            buttonPlaySequence.onClick.RemoveListener(NextRound);
        }

        public void InitRangeDifficulties(STDifficultiesPreset difficultiesPreset)
        {
            gameSettingsManager.difficultiesPreset = difficultiesPreset;
        }

        public void InitWheel(STGamePreset wheelPreset)
        {
            gameSettingsManager.gamePreset = wheelPreset;
            InitializedGame();
        }

        private void Init()
        {
            if (colorValidator == null || gameController == null)
            {
                Debug.LogError("Dependencies not injected!");
                return;
            }

            SubscribeToEvents();
            isInitialized = true;
        }

        private void OnRestartClick()
        {
            gameController.RestartGameEvent?.Invoke();
            historyColor.ClearHistory();
            colorValidator.CleatInputList();
            windowRestratGame.SetActive(false);
            gameController.RestartGameEvent?.Invoke();
        }

        private void OnNextClick()
        {
            historyColor.ClearHistory();
            gameController.SetActivePianino(false);
            buttonPlaySequence.interactable = true;
            windowRestratGame.SetActive(false);
        }

        private void ShowWindowRestartGame()
        {
            windowRestratGame.SetActive(true);
        }

        private void EverythingIsCorrect()
        {
            buttonPlaySequence.interactable = true;
            windowRestratGame.SetActive(false);
            historyColor.ClearHistory();
            gameController.SetActivePianino(false);
        }

        private void InitializedGame()
        {
            builderGame.CreateObject(gameSettingsManager);
        }

        private void StartGame()
        {
            buttonPlaySequence.interactable = false;
            buttonStartGame.gameObject.SetActive(false);
            gameController.StartGameEvent?.Invoke(gameSettingsManager);
        }

        private void NextRound()
        {
            buttonPlaySequence.interactable = false;
            gameController.NextGameEvent?.Invoke();
        }
    }
}
