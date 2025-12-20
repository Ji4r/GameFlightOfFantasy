using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class LoadScreenManager : MonoBehaviour
    {
        [Header("Настройки анимации")]
        [SerializeField] private float duration;
        [SerializeField] private float startValue;
        [SerializeField] private float endValue;


        [SerializeField] private GameObject loadScreen;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textProcess;
        [SerializeField] private GameObject textLoad;

        private LoadScreenAnimation screenAnimation;

        private void Awake()
        {
            screenAnimation = new LoadScreenAnimation(image.material, duration, startValue, endValue);
        }

        public void ShowLoadScreen(Action callbock = null)
        {
            textLoad.SetActive(true);
            screenAnimation.ShowLoadScreen(callbock);
        }

        public void HideLoadScreen(Action callbock = null)
        {
            textLoad.SetActive(false);
            screenAnimation.HideLoadScreen(callbock);
        }

        public void ActiveLoadScreen()
        {
            textLoad.SetActive(true);
            loadScreen.SetActive(true);
            screenAnimation.ResetMaterialValue();
        }

        public async Task ActiveLoadScreenAndShowAnims()
        {
            textLoad.SetActive(true);
            loadScreen.SetActive(true);
            await screenAnimation.ShowLoadScreenAsync();
        }

        public void HideLoadScreenAndShowAnims()
        {
            textLoad.SetActive(false);
            screenAnimation.HideLoadScreen(() =>
            {
                loadScreen.SetActive(false);
            });
        }

        public void UpdateTextProcess(string text)
        {
            textProcess.text = text;
        }
    }
}
