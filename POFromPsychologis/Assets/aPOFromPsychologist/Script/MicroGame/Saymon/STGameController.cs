using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STGameController : GameController
    {
        [Header("Сложность")]
        [SerializeField] private Button button1_4;
        [SerializeField] private Button button2_6;
        [SerializeField] private Button button7;

        [Header("Колличество цветов")]
        [SerializeField] private Button button4;
        [SerializeField] private Button button6;
        [SerializeField] private Button button8;

        [SerializeField] private GameObject prefabColor;
        [SerializeField] private Transform slotColors;
        [SerializeField] private GameObject menuStart;
        [SerializeField] private STSimonWheel simonWheel;

        private int whatCreateColor = 0;
        private Range rangeDifficulties = new();

        private void OnEnable()
        {
            button1_4.onClick.AddListener(() => {
                rangeDifficulties.minValue = 1; 
                rangeDifficulties.maxValue = 4; 
                StartGame(); 
            });
            button2_6.onClick.AddListener(() => {
                rangeDifficulties.minValue = 2;
                rangeDifficulties.maxValue = 6;
                StartGame();
            });
            button7.onClick.AddListener(() => {
                rangeDifficulties.minValue = 7;
                rangeDifficulties.maxValue = 7;
                StartGame();
            });

            button4.onClick.AddListener(() => { whatCreateColor = 4; StartGame(); });
            button6.onClick.AddListener(() => { whatCreateColor = 6; StartGame(); });
            button8.onClick.AddListener(() => { whatCreateColor = 8; StartGame(); });
        }

        private void OnDisable()
        {
            button1_4.onClick.RemoveListener(() => {
                rangeDifficulties.minValue = 1;
                rangeDifficulties.maxValue = 4;
                StartGame();
            });
            button2_6.onClick.RemoveListener(() => {
                rangeDifficulties.minValue = 2;
                rangeDifficulties.maxValue = 6;
                StartGame();
            });
            button7.onClick.RemoveListener(() => {
                rangeDifficulties.minValue = 7;
                rangeDifficulties.maxValue = 7;
                StartGame();
            });

            button4.onClick.RemoveListener(() => { whatCreateColor = 4; StartGame(); });
            button6.onClick.RemoveListener(() => { whatCreateColor = 6; StartGame(); });
            button8.onClick.RemoveListener(() => { whatCreateColor = 8; StartGame(); });
        }

        protected override void StartGame()
        {
            if (whatCreateColor == 0)
                Debug.LogError("Ошибка старта игры нельзя создать 0 цветов");

            for (int i = 0; i < whatCreateColor; i++)
            {
                Debug.Log(whatCreateColor);
                var ColorBtn = Instantiate(prefabColor, slotColors);
            }

            menuStart.SetActive(false);
            simonWheel.StartSimon(rangeDifficulties);
        }

        protected override void NextRound()
        {

        }

        protected override void EndGame()
        {

        }


        protected override void RestartGame()
        {

        }
    }

    public struct Range
    {
        public int minValue;
        public int maxValue;
    }
}
