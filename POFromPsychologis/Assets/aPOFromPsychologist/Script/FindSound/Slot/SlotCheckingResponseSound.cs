using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotCheckingResponseSound : Slot
{
    [SerializeField] private AudioClip theRightSound;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private SlotContainer slotContainer;

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
            CheckingSound(otherItemTransform);
        }
    }


    private void CheckingSound(Transform objectTrans)
    {
        if (objectTrans.TryGetComponent<SoundPayerButton>(out var sound))
        {
            if (sound.GetClip() == theRightSound)
            {
                tmp.color = Color.green;
                tmp.text = "Правильно!";
            }
            else
            {
                tmp.color = Color.red;
                tmp.text = "Ошибка";
            }
        }
    }
}
