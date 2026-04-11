using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    public class M2Slot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private int howManyObjectsCanBeStored;

        public void OnDrop(PointerEventData eventData)
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
}
