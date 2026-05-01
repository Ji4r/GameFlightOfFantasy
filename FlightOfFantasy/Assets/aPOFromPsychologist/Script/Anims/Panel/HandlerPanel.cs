using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    [RequireComponent(typeof(PanelAnims))]
    public class HandlerPanel : MonoBehaviour
    {
        [SerializeField] Button[] buttonsShow;
        [SerializeField] Button[] buttonsHide;
        private PanelAnims panelAnims;

        private void Start()
        {
            panelAnims = GetComponent<PanelAnims>();
        }

        private void OnEnable()
        {
            foreach (var button in buttonsShow)
            {
                if (button != null)
                    button.onClick.AddListener(Show);
            }

            foreach (var button in buttonsHide)
            {
                if (button != null)
                    button.onClick.AddListener(Hide);
            }
        }

        private void OnDisable()
        {
            foreach (var button in buttonsShow)
            {
                if (button != null)
                    button.onClick.RemoveListener(Show);
            }

            foreach (var button in buttonsHide)
            {
                if (button != null)
                    button.onClick.RemoveListener(Hide);
            }
        }

        private void Show()
        {
            panelAnims.Show();
        }

        private void Hide() 
        {
            panelAnims.Hide();
        }
    }
}
