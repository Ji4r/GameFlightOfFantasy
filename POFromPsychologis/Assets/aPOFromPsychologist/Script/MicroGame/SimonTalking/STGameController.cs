using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class STGameController : GameController
    {
        [SerializeField] private GameObjectFactory gameFactory;
        [SerializeField] private Transform slotColors;
        [SerializeField] private GameObject menuStart;
        [SerializeField] private Transform parentButtonColor;

        [Inject] private STSimonWheel simonWheel; 

        public Action RestartGameEvent;
        public Action NextGameEvent;
        public Action<STGameSettingsManager> StartGameEvent;

        private STGameSettingsManager gameManager;
        private List<Color> listColor;


        private void OnEnable()
        {
            RestartGameEvent += RestartGame;
            NextGameEvent += NextRound;
            StartGameEvent += StartGame;
        }

        private void OnDisable()
        {
            RestartGameEvent -= RestartGame;
            NextGameEvent -= NextRound;
            StartGameEvent -= StartGame;
        }

        private void StartGame(STGameSettingsManager gameSettings)
        {
            this.gameManager = gameSettings;
            var listButtonColor = new List<STButtonPianino>();

            if (gameSettings.WhatCreateColor <= 0)
            {
                Debug.LogError("Ошибка старта игры нельзя создать 0 цветов");
                return;
            }

            for (int i = 0; i < gameSettings.WhatCreateColor; i++)
            {
                GameObject colorBtn = gameFactory.CreateColorButton(slotColors);
            }


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
            simonWheel.StartSimon(gameSettings.RangeDifficulties);
        }

        protected override void NextRound()
        {
            if (gameManager.WhatCreateColor == 0)
            {
                Debug.LogError("Ошибка старта игры нельзя создать 0 цветов");
                return;
            }

            simonWheel.NextSimon(gameManager.RangeDifficulties);
        }

        protected override void RestartGame()
        {

        }
    }

    public struct Range
    {
        public int minValue;
        public int maxValue;

        public Range(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}
