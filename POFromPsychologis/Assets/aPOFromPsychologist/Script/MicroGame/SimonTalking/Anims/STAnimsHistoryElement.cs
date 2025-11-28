using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class STAnimsHistoryElement
    {
        private float durationAnims;
        private Sequence sequence;
        private Vector3 startScale;
        private Vector3 startRotate;

        public STAnimsHistoryElement(float durationAnims)
        {
            this.durationAnims = durationAnims;
        }

        public void ShowCardElement(GameObject element, Action collback = null)
        {
            var transformElement = element.transform;
            startScale = transformElement.localScale;
            startRotate = transformElement.localRotation.eulerAngles;
            var endRotate = new Vector3(startRotate.x, startRotate.y, startRotate.z + 180);
            transformElement.localScale = Vector3.zero;
            element.SetActive(true);
            sequence = DOTween.Sequence();

            sequence.Append(transformElement.DOScale(startScale, durationAnims)).
            Join(transformElement.DORotate(endRotate, durationAnims)).OnComplete(() =>
            {
                collback?.Invoke();
            });
        }

        private void HideElement(GameObject element, Action collback = null)
        {
            var transformElement = element.transform;
            startRotate.z += 180;
            transformElement.localScale = Vector3.zero;
            element.SetActive(true);
            sequence = DOTween.Sequence();

            sequence.Append(transformElement.DOScale(startScale, durationAnims)).
            Join(transformElement.DORotate(startRotate, durationAnims)).OnComplete(() => 
            {
                collback?.Invoke();
            });
        }

        public void HideAllElement(List<GameObject> elements, Action collback = null)
        {
            if (elements.Count == 0)
            {
                collback?.Invoke();
                return;
            }

            int completedCount = 0;
            int totalCount = elements.Count;

            foreach (var element in elements)
            {
                HideElement(element, () =>
                {
                    completedCount++;
                    if (completedCount >= totalCount)
                    {
                        collback?.Invoke();
                    }
                });
            }
        }

        public void KillAllAinms()
        {
            sequence?.Kill();
        }
    }
}
