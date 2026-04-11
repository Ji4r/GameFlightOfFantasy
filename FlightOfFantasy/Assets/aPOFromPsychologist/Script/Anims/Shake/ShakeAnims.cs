using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DiplomGames
{
    public class ShakeAnims : IDisposable
    {
        private ScriptableShake presetAnims;
        private List<Tween> tweens;

        public ShakeAnims(ScriptableShake shakeAnims)
        {
            presetAnims = shakeAnims;
            tweens = new List<Tween>();
        }

        public void Dispose()
        {
            if (tweens != null) 
            {
                foreach (var t in tweens)
                {
                    t.Kill();
                }
            }
            tweens.Clear();
        }

        public async Task StartShake(Transform objectForShake)
        {
            Tween tween = objectForShake.DOShakePosition(presetAnims.DurationAnims, presetAnims.PowerShake)
                .SetEase(presetAnims.EaseShake);

            tweens.Add(tween);
            await tween.AsyncWaitForCompletion();
        }
    }
}
