using System;
using System.Collections.Generic;
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
        [SerializeField] private Transform parentButtonColor;

        [SerializeField] private List<Color> listColor;
        [SerializeField] private STColorValidator colorValidator;
        [Inject] private LoadScreenManager loadScreenManager;
        [SerializeField] private SwitchScene switchScene;
        [SerializeField] private GameObject gameObjectScreenload;

        public Action RestartGameEvent;
        public Action NextGameEvent;


        private int whatCreateColor = 0;
        private Range rangeDifficulties = new();

        private void OnEnable()
        {
            RestartGameEvent += RestartGame;
            NextGameEvent += NextRound;

            button1_4.onClick.AddListener(() => {
                rangeDifficulties.minValue = 1; 
                rangeDifficulties.maxValue = 4; 
            });
            button2_6.onClick.AddListener(() => {
                rangeDifficulties.minValue = 2;
                rangeDifficulties.maxValue = 6;
            });
            button7.onClick.AddListener(() => {
                rangeDifficulties.minValue = 7;
                rangeDifficulties.maxValue = 7;
            });

            button4.onClick.AddListener(() => { whatCreateColor = 4; StartGame(); });
            button6.onClick.AddListener(() => { whatCreateColor = 6; StartGame(); });
            button8.onClick.AddListener(() => { whatCreateColor = 8; StartGame(); });
        }

        private void OnDisable()
        {

            RestartGameEvent -= RestartGame;
            NextGameEvent -= NextRound;

            button1_4.onClick.RemoveListener(() => {
                rangeDifficulties.minValue = 1;
                rangeDifficulties.maxValue = 4;
            });
            button2_6.onClick.RemoveListener(() => {
                rangeDifficulties.minValue = 2;
                rangeDifficulties.maxValue = 6;
            });
            button7.onClick.RemoveListener(() => {
                rangeDifficulties.minValue = 7;
                rangeDifficulties.maxValue = 7;
            });

            button4.onClick.RemoveListener(() => { whatCreateColor = 4; StartGame(); });
            button6.onClick.RemoveListener(() => { whatCreateColor = 6; StartGame(); });
            button8.onClick.RemoveListener(() => { whatCreateColor = 8; StartGame(); });
        }

        protected override void StartGame()
        {
            if (whatCreateColor == 0)
            {
                Debug.LogError("Ошибка старта игры нельзя создать 0 цветов");
                return;
            }

            for (int i = 0; i < whatCreateColor; i++)
            {
                var ColorBtn = Instantiate(prefabColor, slotColors);
            }

            var listButtonColor = new List<STButtonPianino>();

            for (int i = 0; i < parentButtonColor.childCount; i++)
            {
                if (parentButtonColor.GetChild(i).TryGetComponent<STButtonPianino>(out var btnColor))
                {
                    listButtonColor.Add(btnColor);
                }
            }

            listColor = simonWheel.GetAllColorWheel();

            if (listButtonColor.Count != listColor.Count)
            {
                Debug.LogError("Колличество кнопок и цветов не соответствует друг другу");
                return;
            }

            for (int i = 0; i < listColor.Count; i++)
            {
                listButtonColor[i].SetColor(listColor[i]);
            }

            menuStart.SetActive(false);
            simonWheel.StartSimon(rangeDifficulties);
        }

        protected override void NextRound()
        {
            if (whatCreateColor == 0)
            {
                Debug.LogError("Ошибка старта игры нельзя создать 0 цветов");
                return;
            }

            simonWheel.NextSimon(rangeDifficulties);
        }

        public void EndGame()
        {
            gameObjectScreenload.SetActive(true);
            loadScreenManager.ShowLoadScreen(() => 
            {
                switchScene.SwitchSceneById(0);
            });
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
