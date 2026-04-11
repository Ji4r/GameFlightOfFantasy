using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomGames
{
    public class STAnimsHistoryElement: IDisposable
    {
        private float durationAnims;
        private Tween tween;
        private Vector3 startScale;

        public STAnimsHistoryElement(float durationAnims)
        {
            this.durationAnims = durationAnims;
        }

        public void Init(Vector3 baseScale)
        {
            this.startScale = baseScale;
        }

        public async Task ShowCardElement(GameObject element)
        {
            var transformElement = element.transform;
            transformElement.localScale = Vector3.zero;
            element.SetActive(true);

            tween = transformElement.DOScale(startScale, durationAnims);
            await tween.AsyncWaitForCompletion();
        }

        private async Task HideElement(GameObject element, Action collback = null)
        {
            var transformElement = element.transform;
            element.SetActive(true);

            tween = transformElement.DOScale(Vector3.zero, durationAnims);
            await tween.AsyncWaitForCompletion();
            collback?.Invoke();
        }

        public async Task HideAllElement(List<GameObject> elements)
        {
            if (elements.Count == 0)
            {
                return;
            }

            int completedCount = 0;
            int totalCount = elements.Count;

            foreach (var element in elements)
            {
                await HideElement(element, () =>
                {
                    completedCount++;
                    if (completedCount >= totalCount)
                    {
                    }
                });
            }
        }

        public void Dispose()
        {
            tween?.Kill();
        }
    }
}
