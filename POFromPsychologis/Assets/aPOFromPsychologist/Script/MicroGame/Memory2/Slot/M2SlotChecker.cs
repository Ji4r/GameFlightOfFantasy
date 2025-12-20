using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    public class M2SlotChecker : CheckerSlot, IDropHandler
    {
        [SerializeField] private int howManyObjectsCanBeStored;
        
        private Sprite rightSprite;
        private bool isReplied;

        public bool IsReplied { get { return isReplied; } }


        public override void CheckRightAnswer(Transform objectTrans)
        {
            if (!GetComponentFromObj(objectTrans, out Image imageComponent))
                return;

            if (imageComponent.sprite == rightSprite)
            {
                Debug.Log("Всё правильно!");

                if (GetComponentFromObj(objectTrans, out M2DragAndDrop dragSystem))
                    dragSystem.enabled = false;
                isReplied = true;
                M2GameManager.instance.CheckIsRepliedRight();
            }
            else
            {
                Debug.Log("Неа");
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
                CheckRightAnswer(otherItemTransform);
            }
        }

        private bool GetComponentFromObj<T>(Transform obj, out T geter) => obj.TryGetComponent<T>(out geter);


        public void Initialize(Transform objectTrans)
        {
            if (!GetComponentFromObj(objectTrans, out Image imageComponent))
                return;

            rightSprite = imageComponent.sprite;
            isReplied = false;
        }

        public void ResetState()
        {
            isReplied = false;
        }
    }
}
