using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace DiplomGames
{
    [RequireComponent(typeof(CanvasGroup), typeof(Image))]
    public class M2DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private RectTransform dragParent; // куда переносим во время драга
        private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        private Vector2 startPosition;
        private Transform originalParent;

        private void Awake()
        {
            dragParent = GameObject.FindGameObjectWithTag("ParentForSlot").GetComponent<RectTransform>();

            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>(); // ✅ фикс

            startPosition = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            rectTransform.DOKill(); // ✅ ВАЖНО

            originalParent = transform.parent;
            startPosition = Vector2.zero; // ✅ фикс опечатки

            transform.SetParent(dragParent);
            transform.SetAsLastSibling();

            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;

            if (dropTarget != null && dropTarget.TryGetComponent<IDropHandler>(out var dropHandler))
            {
                if (dropTarget == gameObject)
                {
                    ReturnToStart();
                }
                else
                {
                    // 👉 если слот сам принял объект — ничего не делаем
                    canvasGroup.blocksRaycasts = true;
                }
            }
            else
            {
                ReturnToStart();
            }
        }

        private void ReturnToStart()
        {
            rectTransform.DOKill(); // ✅ ВАЖНО
            rectTransform.SetParent(originalParent);

            // ✅ правильное возвращение
            rectTransform.DOAnchorPos(startPosition, 0.25f);

            canvasGroup.blocksRaycasts = true;
        }

        public void SetRaycast(bool isActive)
        {
            canvasGroup.blocksRaycasts = isActive;
        }
    }
}