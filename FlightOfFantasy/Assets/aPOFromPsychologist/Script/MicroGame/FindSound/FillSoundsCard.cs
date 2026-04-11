using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DiplomGames
{
    public class FillSoundsCard : MonoBehaviour
    {
        [SerializeField] private SoundAndImage[] allUseAudioClips;

        private Queue<SoundAndImage> shuffleAllClips;
        private Queue<SoundAndImage> shuffleAllOtherClips;

        private void FillQueueWithShuffle()
        {
            shuffleAllClips = new Queue<SoundAndImage>();
            System.Random rand = new System.Random();
            var shuffleSlots = allUseAudioClips.OrderBy(x => rand.Next()).ToArray();

            foreach (var slot in shuffleSlots)
            {
                shuffleAllClips.Enqueue(slot);
            }
        }

        public (AudioClip, Sprite) GetRandomClipOnQueue()
        {
            if (shuffleAllClips == null || shuffleAllClips.Count == 0)
                FillQueueWithShuffle();

            var clips = shuffleAllClips.Dequeue();
            return (clips.clip, clips.sprite);
        }

        public FSSoundList GetRandomClip(int countGetSound, FSSoundList soundGenerated)
        {
            SoundAndImage clip;
            FillQueueWithShuffleOther();

            if (countGetSound > shuffleAllOtherClips.Count)
                Debug.LogError($"Вы берёте больше клипов чем есть в списке {this.gameObject} \n у вас {shuffleAllOtherClips.Count}" +
                    $" а вы пытаетесь взять {countGetSound}");

            for (int i = 0; i < countGetSound; i++)
            {
                clip = shuffleAllOtherClips.Dequeue();

                do
                {
                    if (!soundGenerated.OtherSound.Contains(clip.clip) && soundGenerated.TheRightSound != clip.clip)
                    {
                        soundGenerated.OtherSound.Add(clip.clip);
                        break;
                    }

                    clip = shuffleAllOtherClips.Dequeue();

                } while (shuffleAllOtherClips.Count != 0);
            }

            return soundGenerated;
        }

        private void FillQueueWithShuffleOther()
        {
            ClearShuffleAllOtherClips();
            System.Random rand = new System.Random();
            var shuffleSlots = allUseAudioClips.OrderBy(x => rand.Next()).ToArray();

            foreach (var slot in shuffleSlots)
            {
                shuffleAllOtherClips.Enqueue(slot);
            }
        }

        private void ClearShuffleAllOtherClips()
        {
            if (shuffleAllOtherClips != null)
                shuffleAllOtherClips.Clear();
            else
                shuffleAllOtherClips = new();
        }
    }

    [System.Serializable]
    public class SoundAndImage
    {
        public AudioClip clip;
        public Sprite sprite;
    }
}
