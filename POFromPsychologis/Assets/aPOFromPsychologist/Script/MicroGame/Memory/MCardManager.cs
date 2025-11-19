using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class MCardManager : MonoBehaviour
    {
        public static MCardManager Instance { get; private set; }

        [SerializeField] private MGameController gameManager;
        [SerializeField, Tooltip("Радитель карт")] private Transform parentCard;
        [SerializeField] private int numberCardsOpenAtOneTime = 2;

        private List<MCardProperties> cards = new();
        private List<MCardProperties> openCards = new();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void CheckingTheNumberOpenCards(MCardProperties lastOpenCard)
        {
            for (int i = 0; i < cards.Count; i++) 
            {
                if (cards[i].IsShow && !cards[i].IsFind)
                {
                    openCards.Add(cards[i]);
                }
            }

            if (openCards.Count == numberCardsOpenAtOneTime)
            {
                if (openCards.Count == numberCardsOpenAtOneTime && openCards[0].UniqueId == openCards[1].UniqueId)
                {
                    openCards[0].FindCard();
                    openCards[1].FindCard();

                    foreach (var card in cards)
                    {
                        if (card == openCards[0])
                        {
                            cards.Remove(card);
                            break;
                        }
                    }

                    foreach (var card in cards)
                    {
                        if (card == openCards[1])
                        {
                            cards.Remove(card);
                            break;
                        }
                    }

                    openCards.Clear();

                    if (cards.Count <= 0)
                    {
                        gameManager.EndGameAction?.Invoke();
                    }
                }
                else
                {
                    for (int i = 0; i < openCards.Count; i++)
                    {
                        openCards[i].HideFacialSide();
                    }
                }
            }

            openCards.Clear();
        }

        public void Initialized()
        {
            if (parentCard == null)
                Debug.LogError("parentCard = null");

            cards.Clear();
            Transform card;

            for (int i = 0; i < parentCard.childCount; i++)
            {
                card = parentCard.GetChild(i);
                if (card.TryGetComponent<MCardProperties>(out var cardProp))
                {
                    cards.Add(cardProp);
                }
            }
        }
    }
}
