using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup), typeof(Image))]
public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Image image;
    private CanvasGroup canvasGroup;

    private Vector3 startPosition;

    public bool isActiveSystem;

    private void Awake()
    {
        isActiveSystem = true;
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        startPosition = rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetMouseButton(eventData)) return;
        
        var slotTransform = rectTransform.parent;
        slotTransform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GetMouseButton(eventData)) return;
        
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GetMouseButton(eventData)) return;
        
        rectTransform.DOLocalMove(startPosition, 0.3f);
        SetRaycast(true);
        
    }
    
    private bool GetMouseButton(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return true;
        }

        return false;
    }

    public void SetRaycast(bool value)
    {
        canvasGroup.blocksRaycasts = value;
    }
}
