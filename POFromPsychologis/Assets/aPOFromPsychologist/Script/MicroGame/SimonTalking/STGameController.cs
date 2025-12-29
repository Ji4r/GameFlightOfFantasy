using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class STGameController : GameController
    {
        [Inject] private STSimonWheel simonWheel;
        [Inject] private STBuilderGame builderGame;

        public Action RestartGameEvent;
        public Action NextGameEvent;
        public Action<STGameSettingsManager> StartGameEvent;

        private STGameSettingsManager gameSettings;


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
            this.gameSettings = gameSettings;
            GameObject Wheel = builderGame.GetWheel();
            List<STButtonPianino> listButtonColor = builderGame.GetButtonColor();

            simonWheel.StartSimon(gameSettings.difficultiesPreset.RangeDifficulties);
        }

        protected override void NextRound()
        {
            if (gameSettings.gamePreset.WhatCreateColor == 0)
            {
                Debug.LogError("Ошибка старта игры нельзя создать 0 цветов");
                return;
            }

            simonWheel.NextSimon(gameSettings.difficultiesPreset.RangeDifficulties);
        }

        protected override void RestartGame()
        {

        }
    }

    [System.Serializable]
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
