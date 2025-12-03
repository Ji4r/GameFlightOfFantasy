using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class STButtonPianino : MonoBehaviour
    {
        private Image imageColor;
        private Button btn;
        private Color myColor;

        [Inject] private static STColorValidator colorValidator;

        private void Awake()
        {
            if (colorValidator == null)
                colorValidator = FindFirstObjectByType<STColorValidator>();

            imageColor = GetComponent<Image>();
            btn = GetComponent<Button>();            
        }

        private void OnEnable()
        {
            btn.onClick.AddListener(OnClickPianoButton);
        }

        private void OnDisable()
        {
            btn.onClick.RemoveListener(OnClickPianoButton);
        }

        public void SetColor(Color newColor)
        {
            if (imageColor == null)
            {
                if (gameObject.TryGetComponent<Image>(out imageColor))
                {
                    imageColor.color = newColor;
                    myColor = newColor;
                }
            }
            else 
            {
                imageColor.color = newColor;
                myColor = newColor;
            }
        }

        public void OnClickPianoButton()
        {
            colorValidator.AddItemInListInput(ref myColor);
        }
    }
}
