using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiplomGames
{
    public class ShakeAnims
    {
        private ScriptableShake presetAnims;
        private List<Tween> tweens;

        public ShakeAnims(ScriptableShake shakeAnims)
        {
            presetAnims = shakeAnims;
            tweens = new List<Tween>();
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
