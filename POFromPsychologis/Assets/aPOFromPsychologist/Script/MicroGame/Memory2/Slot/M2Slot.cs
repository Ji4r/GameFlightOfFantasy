using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DiplomGames
{
    public class M2Slot : CheckerSlot, IDropHandler
    {
        [SerializeField] private int howManyObjectsCanBeStored;

        private Sprite rightSprite;

        public override void CheckRightAnswer(Transform objectTrans)
        {
            if (!GetImageFromObj(objectTrans, out Image imageComponent))
                return;

            if (imageComponent.sprite == rightSprite)
            {
                Debug.Log("Всё правильно!");
            }
        }

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

        private bool GetImageFromObj<T>(Transform obj, out T geter) => obj.TryGetComponent<T>(out geter); 


        public void Initialize(Transform objectTrans)
        {
            if (!GetImageFromObj(objectTrans, out Image imageComponent))
                return;

            rightSprite = imageComponent.sprite;
        }
    }
}
