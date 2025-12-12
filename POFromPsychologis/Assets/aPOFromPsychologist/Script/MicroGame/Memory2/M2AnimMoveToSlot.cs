using UnityEngine;
using DG.Tweening;

namespace DiplomGames
{
    public class M2AnimMoveToSlot
    {
        private float duration;
        private Ease ease;

        public M2AnimMoveToSlot(float duration, Ease ease) 
        { 
            this.duration = duration;
            this.ease = ease;
        }


        public void MoveToSlot(Transform image, Transform slot)
        {
            image.DOMove(slot.position, duration).SetEase(ease);
        }
    }
}
