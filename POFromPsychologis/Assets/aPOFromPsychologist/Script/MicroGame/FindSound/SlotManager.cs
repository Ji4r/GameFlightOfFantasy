using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private FillSoundsCard sounds;
        [SerializeField] private UiViewFS uiViewFS;
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

        public FSSoundList StartGame()
        {
            anims.CardMoveToSlot(cards, slots);
            return SelectTheRemainingSounds(sounds.GetRandomClipOnQueue());
        }

        public FSSoundList NextGame()
        {
            anims.CardMoveOnStartPosition(cards, slots, () => 
            {
                anims.CardMoveToSlot(cards, slots);
                uiViewFS.ClearShowOtvet();
            });
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
                    Debug.Log("Длина звука " + soundList.OtherSound[otherSoundIndex].length);
                    otherSoundPlayer.UpdateData(soundList.OtherSound[otherSoundIndex].length);
                    otherSoundIndex++;
                }
            }
        }

        private void OnDisable()
        {
            anims.Dispose();
        }
    }

    public class FSSoundList
    {
        public AudioClip TheRightSound;
        public Sprite Sprite;
        public List<AudioClip> OtherSound = new();
    }
}