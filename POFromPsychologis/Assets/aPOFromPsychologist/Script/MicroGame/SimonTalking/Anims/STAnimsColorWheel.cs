using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DiplomGames
{
    public class STAnimsColorWheel
    {
        private Tween tween;
        private STPresetColorAnimsWheel preset;

        public STAnimsColorWheel(STPresetColorAnimsWheel preset)
        {
            this.preset = preset;
        }


        public async Task StartFullAnims(Image uiElement, Color baseColor, Color endColor)
        {
            if (!IsImageValid(uiElement))
                return;

            await ShowColor(uiElement, baseColor);
            await WaitInterval((int)(preset.ColorDisplayDuration * 1000));

            if (!IsImageValid(uiElement))
                return;
            await HideColor(uiElement, endColor);
        }

        public async Task ShowColor(Image uiElement, Color endColor)
        {
            tween = uiElement.DOColor(endColor, preset.DurationOfColorAppearance);
            await tween.AsyncWaitForCompletion();
        }

        public async Task HideColor(Image uiElement, Color baseColor)
        {
            tween = uiElement.DOColor(baseColor, preset.DurationOfColorAppearance);
            await tween.AsyncWaitForCompletion();
        }

        public async Task WaitInterval(int interval = 0)
        {
            int baseInterval = (int)(preset.IntervalBetweenColors * 1000);
            await Task.Delay(interval <= 0 ? baseInterval : interval);
        }

        private bool IsImageValid(Image image)
        {
            if (image == null || image.Equals(null) || image.gameObject == null || !image.gameObject.activeInHierarchy)
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            if (tween != null)
                tween.Kill();
        }
    }
}
