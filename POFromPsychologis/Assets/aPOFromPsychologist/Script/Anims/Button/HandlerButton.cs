using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    [RequireComponent(typeof(AnimsButton))]
    public class HandlerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        private AnimsButton buttonAnims;

        private void Start()
        {
            buttonAnims = GetComponent<AnimsButton>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonAnims.OnDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonAnims.OnUp();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonAnims.OnExit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonAnims.OnEnter();
        }
    }
}
