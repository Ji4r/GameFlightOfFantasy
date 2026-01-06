using DG.Tweening.Core;
using System;
using System.Collections;
using UnityEngine;

namespace DiplomGames
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer instance;

        [SerializeField, Tooltip("создовать новую очередь? после её окончания")] 
        private bool isGenerateShuffleList = true;
        [SerializeField] private float delayBetweenTracks = 0.1f;
        [SerializeField] private MusicPlaylist musicPlaylist;

        public event Action SwitchedMusic;

        private float volumeMusic;
        private bool isPaused = false;
        private AudioSource source;
        private Coroutine coroutinePlayMusic;

        private void Start()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

            source = GetComponent<AudioSource>();
            source.clip = null;

            musicPlaylist.Initialize();

            if (musicPlaylist != null)
            {
                if (musicPlaylist.Playlist == null || musicPlaylist.Playlist.Count == 0)
                {
                    Debug.LogError("Playlist равен null или он пуст, поэтому музыка не запустится.");
                    return;
                }
            }
            else
            {
                Debug.LogError("musicPlaylist равен null.");
                return;
            }
        }

        private IEnumerator StartLoopPlaylist()
        {
            while (true)
            {
                if (source.clip == null)
                    source.clip = musicPlaylist.GetShuffleClip(isGenerateShuffleList);

                if (!source.isPlaying)
                {
                    source.Play();
                }

                SwitchedMusic?.Invoke();

                yield return new WaitForSeconds(source.clip.length + delayBetweenTracks);

                source.clip = null;
            }
        }

        public void Stop()
        {
            if (coroutinePlayMusic != null)
            {
                StopCoroutine(coroutinePlayMusic);
                coroutinePlayMusic = null;
            }
            source.Stop();
            source.clip = null;
        }

        public void Pause()
        {
            if (source.isPlaying)
            {
                source.Pause();
                isPaused = true;
            }
        }

        public void Resume()
        {
            if (isPaused && !source.isPlaying)
            {
                source.Play();
                isPaused = false;
            }
        }

        public void UnmuteSetNewVolume(float previousVolume)
        {
            ChangeValueMusic(previousVolume);
            Resume();
        }

        /// <summary>
        /// Устонавливает громкость музыки
        /// </summary>
        /// <param name="volume">Принимает значение громкости от 0 до 1</param>
        public void ChangeValueMusic(float volume)
        {
            if (volume < 0 || volume > 1)
            {
                Debug.LogError($"Вы пытаетесь передать значение меньше 0 или больше 1 - {volume}");
                return;
            }

            volumeMusic = volume;
            source.volume = volumeMusic;

            if (!source.isPlaying && volume > 0)
                StartPlay();
        }

        /// <summary>
        /// В зависимости от громкости меняет состояние, если громкость = 0 то 
        /// он не запустит музыку или наооброт остоновит её.
        /// Если больше 0 то запустит музыку
        /// </summary>
        public void StartPlay()
        {
            if (volumeMusic == 0)
            {
                Pause();
                return;
            }
            else
                coroutinePlayMusic = StartCoroutine(StartLoopPlaylist());
        }
    }
}
