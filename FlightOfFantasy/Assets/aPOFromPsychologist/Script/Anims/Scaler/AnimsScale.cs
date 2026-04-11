using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomGames
{
    public class AnimsScale : IDisposable
    {
        private readonly ScriptableScaler scriptableScaler;

        private Tween tween;

        public AnimsScale(ScriptableScaler scriptableScaler)
        {
            this.scriptableScaler = scriptableScaler;
        }

        public void Dispose()
        {
            if (tween != null)
                tween.Kill();
        }

        public async Task SetScale(Transform transform, Vector3 endScale)
        {
            tween = transform.DOScale(endScale, scriptableScaler.DurationAnims).SetEase(scriptableScaler.EaseShake);
            await tween.AsyncWaitForCompletion();
        }
    }
}
