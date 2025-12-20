using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace DiplomGames
{
    [RequireComponent(typeof(CanvasGroup), typeof(Image))]
    public class M2DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private RectTransform parent;
        private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        private Vector3 startPosition;
        private Transform originalParent; 

        private void Awake()
        {
            parent = GameObject.FindGameObjectWithTag("ParentForSlot").GetComponent<RectTransform>();

            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponentInParent<CanvasGroup>();
            startPosition = rectTransform.localPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalParent = transform.parent;
            startPosition = rectTransform.localPosition;

            transform.SetParent(parent);
            transform.SetAsLastSibling();

            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;

            if (dropTarget != null && dropTarget.TryGetComponent<IDropHandler>(out var dropHandler))
            {
                if (dropTarget == gameObject)
                {
                    ReturnObj();
                }
                else
                {
                    rectTransform.DOLocalMove(startPosition, 0.3f);
                    canvasGroup.blocksRaycasts = true;
                }
            }
            else
            {
                ReturnObj();
            }
        }

        private void ReturnObj()
        {
            rectTransform.SetParent(originalParent);
            rectTransform.DOLocalMove(startPosition, 0.3f);
            SetRaycast(true);
        }

        public void SetRaycast(bool isActive)
        {
            canvasGroup.blocksRaycasts = isActive;
        }
    }
}
