using UnityEngine;
using UnityEngine.EventSystems;
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
                button.onClick.AddListener(Show);
            }

            foreach (var button in buttonsHide)
            {
                button.onClick.AddListener(Hide);
            }
        }

        private void OnDisable()
        {
            foreach (var button in buttonsShow)
            {
                button.onClick.RemoveListener(Show);
            }

            foreach (var button in buttonsHide)
            {
                button.onClick.RemoveListener(Hide);
            }
        }

        private void Show()
        {
            SoundPlayer.instance.PlaySound(ListSound.OpeningAWindow);
            panelAnims.Show();
        }

        private void Hide() 
        {
            SoundPlayer.instance.PlaySound(ListSound.OpeningAWindow);
            panelAnims.Hide();
        }
    }
}
