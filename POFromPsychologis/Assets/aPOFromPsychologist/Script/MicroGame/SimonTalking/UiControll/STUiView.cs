using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STUiView : MonoBehaviour
    {
        [SerializeField] private GameObject windowRestratGame;
        [SerializeField] private TextMeshProUGUI txtIsWin;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonNext;

        [Inject] private STColorValidator colorValidator;
        [Inject] private STGameController gameController;

        private bool isInitialized;

        private void OnEnable()
        {
            if (!isInitialized) return;

            SubscribeToEvents();
        }

        private void OnDisable()
        {
            if (!isInitialized) return;

            buttonRestart.onClick.RemoveListener(OnRestartClick);
            buttonNext.onClick.RemoveListener(OnNextClick);
            colorValidator.AnErrorWasMade -= ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect -= EverythingIsCorrect;
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

        private void SubscribeToEvents()
        {
            buttonRestart.onClick.AddListener(OnRestartClick);
            buttonNext.onClick.AddListener(OnNextClick);
            colorValidator.AnErrorWasMade += ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect += EverythingIsCorrect;
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
    }
}
