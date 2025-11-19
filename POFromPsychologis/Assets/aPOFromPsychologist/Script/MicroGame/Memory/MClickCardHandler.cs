using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MClickCardHandler : MonoBehaviour, IPointerClickHandler
    {
        private MCardProperties mCardProperties;

        private void Awake()
        {
            if (!TryGetComponent<MCardProperties>(out mCardProperties))
            {
                Debug.LogError("Не наден MCardPropertis");
            }
        }   

        public void OnPointerClick(PointerEventData eventData)
        {
            if (mCardProperties.IsShow || mCardProperties.IsFind) 
            { 
                return; 
            }

            mCardProperties.ShowFacialSide(() => {
                MCardManager.Instance.CheckingTheNumberOpenCards(mCardProperties);
            });                  
        }
    }
}
