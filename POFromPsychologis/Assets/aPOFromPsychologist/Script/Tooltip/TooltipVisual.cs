using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class TooltipVisual : MonoBehaviour
    {
        [SerializeField] private Camera mCamera;
        [SerializeField] private RectTransform window;
        [SerializeField] private Image imagePanel;
        [SerializeField] private TextMeshProUGUI textText;
        [SerializeField] private float durationAnims;

        private AnimsTooltip animsTooltip;

        private void Start()
        {
            animsTooltip = new(durationAnims, window, imagePanel, textText);
        }

        public void ShowTooltip(string text)
        {
            textText.text = text;
            float textPaddingSize = 4;
            Vector2 backgroundSize = new Vector2(textText.preferredWidth + textPaddingSize * 2f, textText.preferredHeight + textPaddingSize * 2); 
            window.sizeDelta = backgroundSize;
            animsTooltip.Show(textText);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(mCamera.transform.position.z); // или конкретное значение
            window.position = mCamera.ScreenToWorldPoint(mousePos);
        }

        public void HideTooltip()
        {
            animsTooltip.Hide(textText);
        }
    }
}
