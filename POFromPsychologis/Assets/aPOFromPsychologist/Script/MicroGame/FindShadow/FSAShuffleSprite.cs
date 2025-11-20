using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DiplomGames
{
    public class FSAShuffleSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] listSprites;

        private Queue<Sprite> shuffleAllSprites;
        private Queue<Sprite> shuffleAllOtherSprites;

        private void FillQueueWithShuffle()
        {
            shuffleAllSprites = new();
            System.Random rand = new System.Random();
            var shuffleSlots = listSprites.OrderBy(x => rand.Next()).ToArray();

            foreach (var slot in shuffleSlots)
            {
                shuffleAllSprites.Enqueue(slot);
            }
        }

        public Sprite GetRandomSprite()
        {
            if (shuffleAllSprites == null || shuffleAllSprites.Count == 0)
                FillQueueWithShuffle();

            var sprite = shuffleAllSprites.Dequeue();
            return sprite;
        }

        public List<Sprite> GetRandomSprites(int countGetSound, Sprite theRightSprite)
        {
            List<Sprite> list = new List<Sprite>();
            Sprite sprite;
            FillQueueWithShuffleOther();

            if (countGetSound > shuffleAllOtherSprites.Count)
                Debug.LogError($"Вы берёте больше клипов чем есть в списке {this.gameObject} \n у вас {shuffleAllOtherSprites.Count}" +
                    $" а вы пытаетесь взять {countGetSound}");

            for (int i = 0; i < countGetSound; i++)
            {
                sprite = shuffleAllOtherSprites.Dequeue();

                do
                {
                    if (theRightSprite != sprite)
                    {
                        list.Add(sprite);
                        break;
                    }

                    sprite = shuffleAllOtherSprites.Dequeue();

                } while (shuffleAllOtherSprites.Count != 0);
            }

            return list;
        }

        private void FillQueueWithShuffleOther()
        {
            ClearShuffleAllOtherClips();
            System.Random rand = new System.Random();
            var shuffleSlots = listSprites.OrderBy(x => rand.Next()).ToArray();

            foreach (var slot in shuffleSlots)
            {
                shuffleAllOtherSprites.Enqueue(slot);
            }
        }

        private void ClearShuffleAllOtherClips()
        {
            if (shuffleAllOtherSprites != null)
                shuffleAllOtherSprites.Clear();
            else
                shuffleAllOtherSprites = new();
        }
    }
}
