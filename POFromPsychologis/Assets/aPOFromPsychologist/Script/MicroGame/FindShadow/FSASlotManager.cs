using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class FSASlotManager : MonoBehaviour
    {
        [SerializeField] private FSAShuffleSprite shuffleSprite;
        [SerializeField] private FSAUiView uiView;
        [SerializeField] private float durationAnims = 0.7f;
        [SerializeField] ScriptableShake shakeAnims = null;

        [Header("Настройки карточки")]
        [SerializeField] private Transform pointStartCard;
        [SerializeField] private Transform[] cards;

        [Header("Настройки слотов")]
        [SerializeField] private Transform mainSlot;
        [SerializeField] private Transform[] slots;

        private FSAnimatedCards anims;
        private ShakeAnims shakeAnim;

        private void Awake()
        {
            anims = new(pointStartCard, durationAnims);
            shakeAnim = new ShakeAnims(shakeAnims);
        }

        public (Sprite, Transform) StartGame()
        {
            return GeneratedNewLevel();
        }

        public async Task<(Sprite, Transform)> NextGame() 
        {
            SetActiveDragCardMove(false);
            await anims.CardMoveOnStartPosition(cards, slots);
            await anims.CardsMoveToSlot(cards, slots);
            SetActiveDragCardMove(true);
            uiView.ClearAnswer();

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

        public async Task StartShake(Transform objectTrans)
        {
            SetActiveDragCardMove(false);
            await shakeAnim.StartShake(objectTrans);
            await anims.CardMoveToSlot(objectTrans, GetFreeSlot());
            SetActiveDragCardMove(true);
        }

        private void SetActiveDragCardMove(bool isEnabled)
        {
            foreach (var card in cards)
            {
                if (card.TryGetComponent<DragAndDrop>(out var drag))
                {
                    drag.enabled = isEnabled;
                    drag.SetRaycast(true);
                }
                if (card.TryGetComponent<HandlerButton>(out var handler))
                {
                    if (!isEnabled)
                        handler.Reset();
                    handler.enabled = isEnabled;
                }
            }
        }

        public Transform GetFreeSlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].childCount < 1)
                {
                    return slots[i];
                }
            }
            throw new System.Exception("Error все слоты полные");
        }

        private void OnDisable()
        {
           anims.Dispose();
        }
    }
}
