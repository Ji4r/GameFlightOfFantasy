using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class FSASlotManager : MonoBehaviour
    {
        [SerializeField] private FSAShuffleSprite shuffleSprite;
        [SerializeField] private FSAUiView uiView;
        [SerializeField] private float durationAnims = 0.7f;

        [Header("Настройки карточки")]
        [SerializeField] private Transform pointStartCard;
        [SerializeField] private Transform[] cards;

        [Header("Настройки слотов")]
        [SerializeField] private Transform mainSlot;
        [SerializeField] private Transform[] slots;

        private FSAnimatedCards anims;

        private void Awake()
        {
            anims = new(pointStartCard, durationAnims);
        }

        public (Sprite, Transform) StartGame()
        {
            return GeneratedNewLevel();
        }

        public (Sprite, Transform) NextGame() 
        {
            anims.CardMoveOnStartPosition(cards, slots, () =>
            {
                anims.CardMoveToSlot(cards, slots);
                uiView.ClearAnswer();
            });
            return GeneratedNewLevel();
        }

        private (Sprite, Transform) GeneratedNewLevel()
        {
            var theRightSprite = shuffleSprite.GetRandomSprite();
            var listSprites = shuffleSprite.GetRandomSprites(3, theRightSprite);
            var transformRight = FillingSlotsWithSprite(listSprites, theRightSprite);
            return (theRightSprite, transformRight);
        }

        private Transform FillingSlotsWithSprite(List<Sprite> spriteList, Sprite theRightSprite)
        {
            int rightSoundIndex = Random.Range(0, cards.Length);
            var rightSpriteCard = cards[rightSoundIndex];

            if (rightSpriteCard.TryGetComponent<Image>(out var imageSlot))
            {
                imageSlot.sprite = theRightSprite;
            }

            int otherSoundIndex = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (i == rightSoundIndex) continue; 

                var otherCard = cards[i];
                if (otherCard.TryGetComponent<Image>(out var otherSprite))
                {
                    otherSprite.sprite = spriteList[otherSoundIndex];
                    otherSoundIndex++;
                }
            }

            return rightSpriteCard;
        }

        private void OnDisable()
        {
           anims.Dispose();
        }
    }
}
