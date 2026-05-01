using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace DiplomGames
{
    public class FSASlotCheckingResponse : MonoBehaviour, IDropHandler
    {
        [SerializeField] private float durationAnims = 0.2f;
        [SerializeField] private SlotContainer slotContainer;
        [SerializeField] private CheckerSlot controller;
        [SerializeField] private byte countChildren = 2;

        public async void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                if (GetMouseButton(eventData)) return;

                if (transform.childCount >= countChildren)
                {
                    slotContainer.MoveSlot(
                        slotContainer.GetFreeSlot(),
                        transform.GetChild(1).transform
                    );
                }
            }
        }

        public bool GetMouseButton(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return true;
            }

            return false;
        }
    }
}
