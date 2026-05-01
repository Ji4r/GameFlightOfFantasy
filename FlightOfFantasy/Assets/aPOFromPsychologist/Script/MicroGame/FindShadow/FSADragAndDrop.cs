using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DiplomGames
{
    [RequireComponent(typeof(CanvasGroup), typeof(Image))]
    public class FSADragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private float duration = 0.2f;

        public bool isInCheckingSlot = false;
        public bool droppedInCheckingSlot = false;

        private RectTransform rectTransform;
        private Canvas canvas;
        private CanvasGroup canvasGroup;


        private Transform startParent;
        private Vector3 startPosition;
        private Vector3 startScale;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();

            startParent = transform.parent;
            startPosition = rectTransform.localPosition;
            startScale = rectTransform.localScale;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            startParent = transform.parent;

            SetRaycast(false);

            rectTransform.DOKill();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            rectTransform.DOKill();

            rectTransform.DOLocalMove(startPosition, 0.3f);

            rectTransform.DOScale(startScale, duration)
                .SetEase(Ease.InBack);

            SetRaycast(true);
        }

        public void SetRaycast(bool value)
        {
            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = value;
        }
    }
}