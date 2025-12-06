using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STUiView : MonoBehaviour
    {
        [Header("Сложность")]
        [SerializeField] private Button buttonDiffecalty1_4;
        [SerializeField] private Button buttonDiffecalty2_6;
        [SerializeField] private Button buttonDiffecalty7;

        [Header("Колличество цветов")]
        [SerializeField] private Button button4Color;
        [SerializeField] private Button button6Color;
        [SerializeField] private Button button8Color;

        [SerializeField] private GameObject windowRestratGame;
        [SerializeField] private TextMeshProUGUI txtIsWin;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonNext;

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

            buttonDiffecalty1_4.onClick.AddListener(() => {
                gameSettingsManager.RangeDifficulties = new Range(1,4);
            });
            buttonDiffecalty2_6.onClick.AddListener(() => {
                gameSettingsManager.RangeDifficulties = new Range(2, 6);
            });
            buttonDiffecalty7.onClick.AddListener(() => {
                gameSettingsManager.RangeDifficulties = new Range(7,7);
            });

            button4Color.onClick.AddListener(() => { gameSettingsManager.WhatCreateColor = 4; StartGame(); });
            button6Color.onClick.AddListener(() => { gameSettingsManager.WhatCreateColor = 6; StartGame(); });
            button8Color.onClick.AddListener(() => { gameSettingsManager.WhatCreateColor = 8; StartGame(); });
        }

        private void OnDisable()
        {
            if (!isInitialized) return;

            buttonRestart.onClick.RemoveListener(OnRestartClick);
            buttonNext.onClick.RemoveListener(OnNextClick);
            colorValidator.AnErrorWasMade -= ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect -= EverythingIsCorrect;

            buttonDiffecalty1_4.onClick.RemoveListener(() => {
                gameSettingsManager.RangeDifficulties = new Range(1, 4);
            });
            buttonDiffecalty2_6.onClick.RemoveListener(() => {
                gameSettingsManager.RangeDifficulties = new Range(2, 6);
            });
            buttonDiffecalty7.onClick.RemoveListener(() => {
                gameSettingsManager.RangeDifficulties = new Range(7, 7);
            });

            button4Color.onClick.RemoveListener(() => { gameSettingsManager.WhatCreateColor = 4; StartGame(); });
            button6Color.onClick.RemoveListener(() => { gameSettingsManager.WhatCreateColor = 6; StartGame(); });
            button8Color.onClick.RemoveListener(() => { gameSettingsManager.WhatCreateColor = 8; StartGame(); });
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

        private void StartGame()
        {
            gameController.StartGameEvent?.Invoke(gameSettingsManager);
        }
    }
}
