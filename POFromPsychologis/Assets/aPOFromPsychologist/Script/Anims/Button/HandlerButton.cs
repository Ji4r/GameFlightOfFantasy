using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    [RequireComponent(typeof(AnimsButtonSize))]
    public class HandlerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private bool isUsedRotate = false;
        [SerializeField] private AnimsButtonRotate animsRoatte;
        private AnimsButtonSize buttonAnims;

        private void Start()
        {
            buttonAnims = GetComponent<AnimsButtonSize>();
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


            if (isUsedRotate)
                animsRoatte.OnExit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonAnims.OnEnter();

            if (isUsedRotate)
                animsRoatte.OnEnter();
        }
    }
}
