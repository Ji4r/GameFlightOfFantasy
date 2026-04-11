using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    [RequireComponent(typeof(AnimsButtonSize))]
    public class HandlerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        private IAnimsButton[] buttonAnims;

        private void Awake()
        {
            buttonAnims = GetComponents<IAnimsButton>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var anim in buttonAnims)
            {
                anim.OnDown();
            }
            SoundPlayer.instance.PlaySound(ListSound.buttonClick);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (var anim in buttonAnims)
            {
                anim.OnUp();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (var anim in buttonAnims)
            {
                anim.OnExit();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (var anim in buttonAnims)
            {
                anim.OnEnter();
            }
            SoundPlayer.instance.PlaySound(ListSound.buttonEnter);
        }

        public void Reset()
        {
            OnPointerExit(null);
        }
    }
}
