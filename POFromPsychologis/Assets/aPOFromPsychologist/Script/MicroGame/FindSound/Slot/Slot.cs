using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Slot : MonoBehaviour, IDropHandler
{
    public abstract void OnDrop(PointerEventData eventData);
}
