using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STUiView : MonoBehaviour
    {
        [SerializeField] private STColorValidator colorValidator;
        [SerializeField] private GameObject windowRestratGame;
        [SerializeField] private TextMeshProUGUI txtIsWin;
        [SerializeField] private STGameController gameController;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonNext;

        private void OnEnable()
        {
            buttonRestart.onClick.AddListener(() => { gameController.RestartGameEvent?.Invoke(); windowRestratGame.SetActive(false); });
            buttonNext.onClick.AddListener(() => { gameController.NextGameEvent?.Invoke(); windowRestratGame.SetActive(false); });
            colorValidator.AnErrorWasMade += ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect += EverythingIsCorrect;
        }

        private void OnDisable()
        {
            buttonRestart.onClick.RemoveListener(() => { gameController.RestartGameEvent?.Invoke(); windowRestratGame.SetActive(false); });
            buttonNext.onClick.RemoveListener(() => { gameController.NextGameEvent?.Invoke(); windowRestratGame.SetActive(false); });
            colorValidator.AnErrorWasMade -= ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect -= EverythingIsCorrect;
        }

        private void ShowWindowRestartGame()
        {
            windowRestratGame.SetActive(true);
        }

        private void EverythingIsCorrect()
        {
            Debug.Log("You Won");
            txtIsWin.text = "You Won";
            gameController.NextGameEvent?.Invoke();
            windowRestratGame.SetActive(false);
        }
    }
}
