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

        public Sprite StartGame()
        {

            return GeneratedNewLevel();
        }

        public Sprite NextGame() 
        {
            return GeneratedNewLevel();
        }

        private Sprite GeneratedNewLevel()
        {
            var theRightSprite = shuffleSprite.GetRandomSprite();
            var listSprites = shuffleSprite.GetRandomSprites(3, theRightSprite);
             //FillingSlotsWithSounds(listSprites);
            return theRightSprite;
        }

        //private void FillingSlotsWithSounds(List<Sprite> soundList)
        //{
        //    int rightSoundIndex = Random.Range(0, cards.Length);
        //    var rightSoundCard = cards[rightSoundIndex];

        //    if (rightSoundCard.TryGetComponent<Image>(out var soundPlayer))
        //    {
        //        soundPlayer.sprite = soundList.TheRightSound;
        //        soundPlayer.UpdateData();
        //    }

        //    int otherSoundIndex = 0;
        //    for (int i = 0; i < cards.Length; i++)
        //    {
        //        if (i == rightSoundIndex) continue;

        //        var otherCard = cards[i];
        //        if (otherCard.TryGetComponent<SoundPayerButton>(out var otherSoundPlayer))
        //        {
        //            otherSoundPlayer.clip = soundList.OtherSound[otherSoundIndex];
        //            Debug.Log("Длина звука " + soundList.OtherSound[otherSoundIndex].length);
        //            otherSoundPlayer.UpdateData(soundList.OtherSound[otherSoundIndex].length);
        //            otherSoundIndex++;
        //        }
        //    }
        //}

        private void OnDisable()
        {
           // anims.Dispose();
        }
    }
}
