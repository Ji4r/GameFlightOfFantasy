using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DiplomGames
{
    public class MGeneratedLevel : MonoBehaviour
    {
        [SerializeField] private List<Sprite> cards;
        [SerializeField] private GameObject prefabCard;
        [SerializeField] private Transform parentCards;

        public void GenerateLevel(int size)
        {
            DeleteChildren();
            var queueCard = CreateShufflePair(PairSelection(size));
            var countQueue =  queueCard.Count;
            for (int i = 0; i < countQueue; i++)
            {
                int uniqId = queueCard.Peek().Item2;
                Sprite sprite = queueCard.Dequeue().Item1;

                var newCard = Instantiate(prefabCard, parentCards);
                if (newCard.TryGetComponent<MCardProperties>(out var cardProp))
                {
                    cardProp.SetSpriteOnBackSide(sprite);
                    cardProp.SetUniqueId(uniqId);
                }
            }

            MCardManager.Instance.Initialized();
        }

        private void DeleteChildren()
        {
            foreach (Transform child in parentCards)
            {
                Destroy(child.gameObject);
            }
        }

        private List<(Sprite, int)> PairSelection(int size)
        {
            System.Random rand = new System.Random();
            var shuffleSprite = cards.OrderBy(x => rand.Next()).ToArray();
            List<(Sprite, int)> shuffleQuue = new(); 

            for (int i = 0; i < size / 2; i++) 
            {
                shuffleQuue.Add((shuffleSprite[i], i));
            }

            return shuffleQuue;
        }

        private Queue<(Sprite, int)> CreateShufflePair(List<(Sprite, int)> listSprite)
        {
            var doubledList = new List<(Sprite, int)>();
            doubledList.AddRange(listSprite);
            doubledList.AddRange(listSprite);

            System.Random rand = new System.Random();
            var shuffleSprite = doubledList.OrderBy(x => rand.Next()).ToArray();

            Queue<(Sprite, int)> shuffleQuue = new();

            foreach (var sprite in shuffleSprite)
            {
                shuffleQuue.Enqueue(sprite);
            }

            return shuffleQuue;
        }
    }
}
