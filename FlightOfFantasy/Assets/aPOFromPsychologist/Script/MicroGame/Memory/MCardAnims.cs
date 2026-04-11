using UnityEngine;
using DG.Tweening;
using System;

namespace DiplomGames
{
    public class MCardAnims
    {
        private Tween firstPart;
        private Tween secondPart;
        private float duration;

        public MCardAnims(float duration)
        {
            this.duration = duration;
        }

        public void RotateCard(Transform frontCard, Transform backCard, Action callback = null)
        {
            KillAnims();
            backCard.gameObject.SetActive(false);

            firstPart = frontCard.DOLocalRotate(new Vector3(frontCard.localRotation.x, 90, frontCard.localRotation.x), duration).OnComplete(() =>
            {
                backCard.localEulerAngles = new Vector3(0, 90, 0);
                backCard.gameObject.SetActive(true);
                frontCard.gameObject.SetActive(false);
                secondPart = backCard.DOLocalRotate(Vector3.zero, duration).OnComplete(() => 
                {
                    callback?.Invoke();
                });
            });
        }

        public void KillAnims()
        {
            firstPart?.Kill();
            secondPart?.Kill();
            firstPart = null;
            secondPart = null;
        }
    }
}
