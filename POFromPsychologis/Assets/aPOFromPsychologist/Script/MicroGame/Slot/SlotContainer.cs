using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class SlotContainer : MonoBehaviour
{
    [SerializeField] private List<Transform> slotWithoutVerification;

    public Transform GetFreeSlot()
    {
        if (slotWithoutVerification.Count <= 0)
        {
            Debug.LogError("Список пуст");
            return null;
        }

        foreach (var slot in slotWithoutVerification)
        { 
            if (slot.childCount == 0)
            {
                return slot;
            }
        }

        Debug.LogError("Не нашолся пустой слот");
        return null;
    }

    public void MoveSlot(Transform freeSlot, Transform currentSlot)
    {
        currentSlot.SetParent(freeSlot);
        currentSlot.DOLocalMove(Vector3.zero, 0.3f);
    }
}
