using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    public class SlotCheckingResponseSound : Slot
    {
        [SerializeField] private SlotContainer slotContainer;
        [SerializeField] private CheckerSlot controller;

        public override void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                if (transform.childCount >= 2)
                {
                    slotContainer.MoveSlot(slotContainer.GetFreeSlot(), transform.GetChild(1).transform);
                }

                var otherItemTransform = eventData.pointerDrag.transform;
                otherItemTransform.SetParent(transform);
                otherItemTransform.localPosition = Vector3.zero;
                controller.CheckingSound(otherItemTransform);
            }
        }
    }
}
