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
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonNext;
        [SerializeField] private Button startGame;

        [Inject] private STBuilderGame builderGame;
        [Inject] private STColorValidator colorValidator;
        [Inject] private STGameController gameController;
        [Inject] private STGameSettingsManager gameSettingsManager;

        private bool isInitialized;

        private void OnEnable()
        {
            if (!isInitialized) return;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            buttonRestart.onClick.AddListener(OnRestartClick);
            buttonNext.onClick.AddListener(OnNextClick);
            colorValidator.AnErrorWasMade += ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect += EverythingIsCorrect;
            startGame.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            if (!isInitialized) return;

            buttonRestart.onClick.RemoveListener(OnRestartClick);
            buttonNext.onClick.RemoveListener(OnNextClick);
            colorValidator.AnErrorWasMade -= ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect -= EverythingIsCorrect;
            startGame.onClick.RemoveListener(StartGame);
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
            windowRestratGame.SetActive(false);
        }

        private void OnNextClick()
        {
            gameController.NextGameEvent?.Invoke();
            windowRestratGame.SetActive(false);
        }

        private void ShowWindowRestartGame()
        {
            windowRestratGame.SetActive(true);
        }

        private void EverythingIsCorrect()
        {
            Debug.Log("You Won");
            gameController.NextGameEvent?.Invoke();
            windowRestratGame.SetActive(false);
        }

        private void InitializedGame()
        {
            builderGame.CreateObject(gameSettingsManager);
        }

        private void StartGame()
        {
            gameController.StartGameEvent?.Invoke(gameSettingsManager);
        }
    }
}
