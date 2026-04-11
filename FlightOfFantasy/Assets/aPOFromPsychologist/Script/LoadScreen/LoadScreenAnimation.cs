using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;

namespace DiplomGames
{
    public class LoadScreenAnimation
    {
        private Material material;
        private float duration;
        private float startValue = 0;
        private float endValue = 1;
        private float defaultValueMaterial;

        public LoadScreenAnimation(Material material, float duration, float startValue, float endValue)
        {
            this.material = material;
            this.defaultValueMaterial = material.GetFloat("_DissolveAmount");
            this.duration = duration;
            this.startValue = startValue;
            this.endValue = endValue;
        }

        public void ShowLoadScreen(Action callbock = null)
        {
            material.DOFloat(startValue, "_DissolveAmount", duration).OnComplete(() =>
            {
                callbock?.Invoke();
            });
        }

        public async Task ShowLoadScreenAsync()
        {
            var tween = material.DOFloat(startValue, "_DissolveAmount", duration);
            await tween.AsyncWaitForCompletion();
        }

        public void HideLoadScreen(Action callbock = null)
        {
            material.DOFloat(endValue, "_DissolveAmount", duration).OnComplete(() =>
            {
                callbock?.Invoke();
            });
        }

        public void ResetMaterialValue()
        {
            material.SetFloat("_DissolveAmount", startValue);
        }
    }
}
