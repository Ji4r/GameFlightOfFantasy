using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    public class HandlerForTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TooltipVisual tooltipVisual;
        [SerializeField, TextArea(1, 4)] public string textTooltip;
        [SerializeField, Tooltip("Через сколько показать подсказку")]
        private float howLongDoesItTakeToShowHint; 


        public void OnPointerEnter(PointerEventData eventData)
        {
            Invoke(nameof(ShowTooltip), howLongDoesItTakeToShowHint);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltipVisual.HideTooltip();
        }

        private void ShowTooltip()
        {
            tooltipVisual.ShowTooltip(textTooltip);
        }
    }
}
