using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    public class SlotCheckingResponse : Slot
    {
        [SerializeField] private SlotContainer slotContainer;
        [SerializeField] private CheckerSlot controller;
        [SerializeField] private byte countChildren = 2;


        public override void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                if (transform.childCount >= countChildren)
                {
                    slotContainer.MoveSlot(slotContainer.GetFreeSlot(), transform.GetChild(1).transform);
                }

                var otherItemTransform = eventData.pointerDrag.transform;
                otherItemTransform.SetParent(transform);
                otherItemTransform.localPosition = Vector3.zero;
                controller.CheckRightAnswer(otherItemTransform);
            }
        }
    }
}
