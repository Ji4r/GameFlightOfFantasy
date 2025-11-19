using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : Slot
{
    [SerializeField] private byte howManyObjectsCanBeStored = 1;

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (transform.childCount >= howManyObjectsCanBeStored)
                return;

            var otherItemTransform = eventData.pointerDrag.transform;
            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
        }
    }
}
