using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Threading.Tasks;

namespace DiplomGames
{
    public class SlotCheckingResponse : MonoBehaviour, IDropHandler
    {
        [SerializeField] private float durationAnims = 0.2f;
        [SerializeField] private SlotContainer slotContainer;
        [SerializeField] private CheckerSlot controller;
        [SerializeField] private byte countChildren = 2;
        public async void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                if (transform.childCount >= countChildren)
                {
                    slotContainer.MoveSlot(slotContainer.GetFreeSlot(), transform.GetChild(1).transform);
                }

                var otherItemTransform = eventData.pointerDrag.transform;
                otherItemTransform.SetParent(transform);
                await otherItemTransform.DOLocalMove(Vector3.zero, durationAnims).AsyncWaitForCompletion();
                //otherItemTransform.localPosition = Vector3.zero;
                controller.CheckRightAnswer(otherItemTransform);
            }
        }
    }
}
