using UnityEngine;

namespace DiplomGames
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        public static SoundPlayer instance { get; private set; }

        private float ValueSong = 1f;
        private AudioSource audioSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this);

            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(ListSound sound)
        {
            audioSource.PlayOneShot(GetAudioClip(sound), ValueSong);
        }


        private AudioClip GetAudioClip(ListSound sound)
        {
            foreach (var song in SoundList.instance.soundAudioClips) 
            {
                if (song.sound == sound)
                    return song.audioClip;
            }

            Debug.LogError($"{sound} Ошибка такого звука нет");
            return null;
        }
    }
}
