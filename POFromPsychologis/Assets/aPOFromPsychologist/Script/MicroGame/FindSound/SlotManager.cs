using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomGames
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private FillSoundsCard sounds;
        [SerializeField] private UiViewFS uiViewFS;
        [SerializeField] private float durationAnims = 0.7f;
        [SerializeField] ScriptableShake shakeAnims;

        [Header("Настройки карточки")]
        [SerializeField] private Transform pointStartCard;
        [SerializeField] private Transform[] cards;

        [Header("Настройки слотов")]
        [SerializeField] private Transform mainSlot;
        [SerializeField] private Transform[] slots;

        private ShakeAnims shakeAnim;
        private FSAnimatedCards anims;

        private void Awake()
        {
            anims = new(pointStartCard, durationAnims);
            shakeAnim = new ShakeAnims(shakeAnims);
        }

        public async Task<FSSoundList> StartGame()
        {
            var a = SelectTheRemainingSounds(sounds.GetRandomClipOnQueue());
            SetActiveDragCardMove(false);
            anims.CardsMoveToSlot(cards, slots);
            SetActiveDragCardMove(true);
            return a;
        }

        public async Task<FSSoundList> NextGame()
        {
            SetActiveDragCardMove(false);
            await anims.CardsMoveOnStartPosition(cards, slots);
            await anims.CardsMoveToSlot(cards, slots);
            uiViewFS.ClearShowOtvet();
            SetActiveDragCardMove(true);

            return SelectTheRemainingSounds(sounds.GetRandomClipOnQueue());
        }

        private FSSoundList SelectTheRemainingSounds((AudioClip, Sprite) TheRightSound)
        {
            var listSound = new FSSoundList();
            listSound.TheRightSound = TheRightSound.Item1;
            listSound.Sprite = TheRightSound.Item2;
            listSound = sounds.GetRandomClip(slots.Length - 1, listSound);
            FillingSlotsWithSounds(listSound);
            return listSound;
        }

        private void FillingSlotsWithSounds(FSSoundList soundList)
        {
            int rightSoundIndex = Random.Range(0, cards.Length);
            var rightSoundCard = cards[rightSoundIndex];

            if (rightSoundCard.TryGetComponent<SoundPayerButton>(out var soundPlayer))
            {
                soundPlayer.clip = soundList.TheRightSound;
                soundPlayer.UpdateData();
            }

            int otherSoundIndex = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (i == rightSoundIndex) continue;

                var otherCard = cards[i];
                if (otherCard.TryGetComponent<SoundPayerButton>(out var otherSoundPlayer))
                {
                    otherSoundPlayer.clip = soundList.OtherSound[otherSoundIndex];
                    otherSoundPlayer.UpdateData(soundList.OtherSound[otherSoundIndex].length);
                    otherSoundIndex++;
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

        private void OnDisable()
        {
            anims.Dispose();
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
    }

    public class FSSoundList
    {
        public AudioClip TheRightSound;
        public Sprite Sprite;
        public List<AudioClip> OtherSound = new();
    }
}