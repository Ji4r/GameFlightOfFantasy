using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Image image;
    private CanvasGroup canvasGroup;

    private Vector3 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        startPosition = rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = rectTransform.parent;
        slotTransform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.DOLocalMove(startPosition, 0.3f);
        canvasGroup.blocksRaycasts = true;
    }
}
