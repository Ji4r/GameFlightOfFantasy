using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private async void StartGame(STGameSettingsManager gameSettings)
        {
            this.gameSettings = gameSettings;
            GameObject Wheel = builderGame.GetWheel();
            List<STButtonPianino> listButtonColor = builderGame.GetButtonColor();

            SetActivePianino(false);
            await simonWheel.StartSimon(gameSettings.difficultiesPreset.RangeDifficulties);
            SetActivePianino(true);
        }

        protected override async void NextRound()
        {
            if (gameSettings.gamePreset.WhatCreateColor == 0)
            {
                Debug.LogError("Ошибка старта игры нельзя создать 0 цветов");
                return;
            }

            SetActivePianino(false);
            await simonWheel.NextSimon(gameSettings.difficultiesPreset.RangeDifficulties);
            SetActivePianino(true);
        }

        protected override void RestartGame()
        {

        }

        public void SetActivePianino(bool isActive)
        {
            foreach (var btn in builderGame.GetButtonColor())
            {
                btn.SetInteractible(isActive);
            }
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
