using UnityEngine;
using DG.Tweening;
using System;

namespace DiplomGames
{
    public class MoveToSlotAnims : IDisposable
    {
        private ScriptableMover preset;
        private Tween tween;

        public MoveToSlotAnims(ScriptableMover preset) 
        { 
            this.preset = preset;
        }

        public void Dispose()
        {
            if (tween != null)
                tween.Kill();
        }

        public void MoveToSlot(Transform image, Transform slot)
        {
           tween = image.DOMove(slot.position, preset.DurationAnims).SetEase(preset.EaseMover);
        }
    }
}
