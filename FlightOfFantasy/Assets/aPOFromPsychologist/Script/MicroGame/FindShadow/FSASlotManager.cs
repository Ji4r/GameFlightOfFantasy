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
        [SerializeField] ScriptableShake shakeAnims;
        [SerializeField] ScriptableScaler scalerPreset;

        [Header("Настройки карточки")]
        [SerializeField] private Transform pointStartCard;
        [SerializeField] private Transform[] cards;

        [Header("Настройки слотов")]
        [SerializeField] private Transform mainSlot;
        [SerializeField] private Transform[] slots;

        private FSAnimatedCards anims;
        private ShakeAnims shakeAnim;
        private AnimsScale animsScale;
        private Vector3[] baseScaleCards;

        private void Awake()
        {
            anims = new(pointStartCard, durationAnims);
            shakeAnim = new ShakeAnims(shakeAnims);
            animsScale = new AnimsScale(scalerPreset);
            baseScaleCards = new Vector3[cards.Length];

            for (int i = 0; i < cards.Length; i++) 
            {
                baseScaleCards[i] = cards[i].localScale;
            }
        }

        public (Sprite, Transform) StartGame()
        {
            return GeneratedNewLevel();
        }

        public async Task NextGame() 
        {
            SetActiveDragCardMove(false);
            var card = GetChildrenFromIdWithMainSlot(1);
            await anims.Awaittime(500);
            await anims.CardMoveToSlot(card, GetFreeSlot());
            SetActiveDragCardMove(true);
            uiView.ClearAnswer();

            //return GeneratedNewLevel();
        }

        public Transform GetChildrenFromIdWithMainSlot(int id)
        {
            if (mainSlot.childCount - 1 < id)
            {
                Debug.Log("Столько детей нету у mainSlot");
                return null;
            }
            return mainSlot.GetChild(id);
        }

        public (Sprite, Transform) GeneratedNewLevel()
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

        public async Task SetScaleToZero()
        {
            Task[] tasks = new Task[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                tasks[i] = animsScale.SetScale(cards[i], Vector3.zero);
            }

            await Task.WhenAll(tasks);
        }

        public async Task SetScaleToBase()
        {
            Task[] tasks = new Task[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                tasks[i] = animsScale.SetScale(cards[i], baseScaleCards[i]);
            }

            await Task.WhenAll(tasks);
        }

        private void OnDisable()
        {
           anims.Dispose();
        }
    }
}
