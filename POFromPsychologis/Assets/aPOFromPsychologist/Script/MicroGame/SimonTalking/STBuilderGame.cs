using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class STBuilderGame : MonoBehaviour
    {
        [Header("Настройки калеса")]
        [SerializeField] private RectTransform parentWheel;
        [SerializeField] private RectTransform pointSpawnWheel;
        [SerializeField] private GameObjectFactory gameFactory;
        [SerializeField] private Transform parentButtonColor;
        [SerializeField] private GameObject menuStart;

        [Inject] private STSimonWheel simonWheel;

        private STGameSettingsManager gameSettings;
        private List<Color> listColor;
        private GameObject Wheel;
        private List<STButtonPianino> listButtonColor;

        public void CreateObject(STGameSettingsManager gameSettings)
        {
            if (gameSettings.gamePreset == null || gameSettings.difficultiesPreset == null)
                throw new System.Exception($"gamePreset - {gameSettings.gamePreset} или difficultiesPreset - {gameSettings.difficultiesPreset} равен null");

            this.gameSettings = gameSettings;
            
            if (listButtonColor != null)
            {
                listButtonColor.Clear();
                Debug.LogWarning($"Массив listButtonColor был создан заново");
            }
            listButtonColor = new List<STButtonPianino>();


            if (Wheel != null)
                Debug.LogWarning($"Переменная Wheel была создана заново");

            Wheel = Instantiate(gameSettings.gamePreset.PrefabWheel, parentWheel);
            Wheel.transform.position = pointSpawnWheel.transform.position;
            Wheel.transform.localRotation = pointSpawnWheel.transform.localRotation;
            InitializeWheel();

            if (gameSettings.gamePreset.WhatCreateColor <= 0)
            {
                throw new System.Exception("Нельзя создать 0 цветов");
            }

            for (int i = 0; i < gameSettings.gamePreset.WhatCreateColor; i++)
            {
                GameObject colorBtn = gameFactory.CreateColorButton(parentButtonColor);
            }

            for (int i = 0; i < parentButtonColor.childCount; i++)
            {
                if (parentButtonColor.GetChild(i).TryGetComponent<STButtonPianino>(out var btnColor))
                {
                    btnColor.SetInteractible(false);
                    listButtonColor.Add(btnColor);
                }
            }

            SetColorPianinoButon();

            menuStart.SetActive(false);
        }

        private void SetColorPianinoButon()
        {
            listColor = simonWheel.GetAllColorWheel();

            if (listButtonColor.Count != listColor.Count)
            {
                throw new System.Exception("Колличество кнопок и цветов не соответствует друг другу");
            }

            for (int i = 0; i < listColor.Count; i++)
            {
                listButtonColor[i].SetColor(listColor[i]);
            }
        }

        private void InitializeWheel()
        {
            if (Wheel.TryGetComponent<SimonWheel>(out var simonWheelRefence))
            {
                simonWheel.parentColorSimon = simonWheelRefence.ReferenceOnContents;
            }
            else
            {
                Debug.LogError("Не найдке SimonWheel у префаба");
                return;
            }
        }

        public GameObject GetWheel()
        {
            if (Wheel == null)
                Debug.LogWarning("Wheel равен null");
            return Wheel;
        }
        public List<STButtonPianino> GetButtonColor()
        {
            if (listButtonColor == null)
                Debug.LogWarning("listButtonColor равен null");
            return listButtonColor;
        }
    }
}
