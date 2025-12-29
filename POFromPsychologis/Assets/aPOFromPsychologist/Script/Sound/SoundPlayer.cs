using UnityEngine;

namespace DiplomGames
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        public static SoundPlayer instance { get; private set; }

        private float ValueSong = 1f;
        private AudioSource audioSource;
        private AudioSource currentLoopingSource; // Для отслеживания текущего звука

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

        public AudioSource PlayWithStop(AudioClip clip)
        {
            StopCurrentSound();

            GameObject soundObject = new GameObject("TemporarySound");
            soundObject.transform.SetParent(transform);
            AudioSource newSource = soundObject.AddComponent<AudioSource>();

            newSource.clip = clip;
            newSource.volume = ValueSong;
            newSource.Play();
            Destroy(soundObject, clip.length);

            currentLoopingSource = newSource;
            return newSource;
        }

        public void PlayShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip, ValueSong);
        }

        public void StopCurrentSound()
        {
            if (currentLoopingSource != null && currentLoopingSource.isPlaying)
            {
                Destroy(currentLoopingSource.gameObject);
                currentLoopingSource = null;
            }
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
