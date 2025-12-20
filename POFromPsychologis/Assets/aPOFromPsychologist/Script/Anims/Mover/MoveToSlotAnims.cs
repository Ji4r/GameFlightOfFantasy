using UnityEngine;
using DG.Tweening;

namespace DiplomGames
{
    public class MoveToSlotAnims
    {
        private ScriptableMover preset;

        public MoveToSlotAnims(ScriptableMover preset) 
        { 
            this.preset = preset;
        }

        public void MoveToSlot(Transform image, Transform slot)
        {
            image.DOMove(slot.position, preset.DurationAnims).SetEase(preset.EaseMover);
        }
    }
}
