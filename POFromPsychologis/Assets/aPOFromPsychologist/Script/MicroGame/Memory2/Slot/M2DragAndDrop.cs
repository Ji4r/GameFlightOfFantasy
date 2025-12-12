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
        private Image image;
        private CanvasGroup canvasGroup;

        private Vector3 startPosition;
        private Transform originalParent; // Сохраняем оригинального родителя
        private int originalSiblingIndex; // Сохраняем оригинальный индекс среди дочерних объектов

        private void Awake()
        {
            parent = GameObject.FindGameObjectWithTag("ParentForSlot").GetComponent<RectTransform>();

            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponentInParent<CanvasGroup>();
            startPosition = rectTransform.localPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalParent = transform.parent;
            startPosition = rectTransform.localPosition;
            originalSiblingIndex = transform.GetSiblingIndex();

            // Меняем родителя на Game (ParentForSlot)
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
            canvasGroup.blocksRaycasts = true;
        }
    }
}
