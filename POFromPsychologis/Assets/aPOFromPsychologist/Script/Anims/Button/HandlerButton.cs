using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    [RequireComponent(typeof(AnimsButtonSize))]
    public class HandlerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        private IAnimsButton[] buttonAnims;

        private void Start()
        {
            buttonAnims = GetComponents<IAnimsButton>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var anim in buttonAnims)
            {
                anim.OnDown();
            }
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
        }
    }
}
