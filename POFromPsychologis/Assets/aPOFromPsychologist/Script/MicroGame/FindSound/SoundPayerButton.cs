using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DiplomGames
{
    public class SoundPayerButton : MonoBehaviour, IPointerClickHandler
    {
        public AudioClip clip;
        [SerializeField] private AudioSource soundPlayer;
        [SerializeField] private Image progressBar;
        [SerializeField] private Image pictureIconPlay;
        [SerializeField] private Sprite picturePlay;
        [SerializeField] private Sprite pictureStop;

        private bool isPlay;
        private float audioDuration;
        private Coroutine coroutineBar;

        private void Start()
        {
            pictureIconPlay.sprite = picturePlay;
            isPlay = false;
            audioDuration = clip != null ? clip.length : 1f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isPlay = !isPlay;

            if (soundPlayer.clip == clip)
            {
                if (isPlay)
                {
                    pictureIconPlay.sprite = pictureStop;
                    soundPlayer.Play();
                    coroutineBar = StartCoroutine(UpdateProgressBar());
                }
                else
                {
                    soundPlayer.Stop();
                    pictureIconPlay.sprite = picturePlay;
                    if (coroutineBar != null)
                        StopCoroutine(coroutineBar);

                    progressBar.fillAmount = 0;
                }
            }
            else
            {
                soundPlayer.clip = clip;
                soundPlayer.Play();
                pictureIconPlay.sprite = pictureStop;
                progressBar.fillAmount = 0;
                coroutineBar = StartCoroutine(UpdateProgressBar());
            }
        }

        public AudioClip GetClip()
        {
            return clip;
        }

        public void UpdateData(float clipLength = 0)
        {
            pictureIconPlay.sprite = picturePlay;
            isPlay = false;
            progressBar.fillAmount = 0;

            if (clip != null)
            {
                audioDuration = clipLength > 0 ? clipLength : clip.length;
            }
        }

        private IEnumerator UpdateProgressBar()
        {
            while (soundPlayer.time < audioDuration)
            {
                if (soundPlayer.clip == clip && isPlay)
                {
                    yield return new WaitForEndOfFrame();
                    progressBar.fillAmount = soundPlayer.time / audioDuration;
                }
                else
                {
                    progressBar.fillAmount = 0;
                    isPlay = false;
                    break;
                }
            }

            progressBar.fillAmount = 0;
            pictureIconPlay.sprite = picturePlay;
        }


        public void StopPlay()
        {
            soundPlayer.Stop();
        }
    }
}

