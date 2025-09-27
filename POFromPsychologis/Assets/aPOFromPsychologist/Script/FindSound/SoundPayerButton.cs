using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundPayerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip clip;
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
        audioDuration = clip.length;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isPlay = !isPlay;

        if (soundPlayer.clip == clip)
        {
            if (isPlay)
            {
                Debug.Log("Блок 2 if");
                pictureIconPlay.sprite = pictureStop;
                soundPlayer.Play();
                coroutineBar = StartCoroutine(UpdateProgressBar());             
            }
            else
            {
                Debug.Log("Блок 2 Else");
                soundPlayer.Stop();
                pictureIconPlay.sprite = picturePlay;
                if (coroutineBar != null)
                    StopCoroutine(coroutineBar);

                progressBar.fillAmount = 0;
            }
        }
        else
        {
            Debug.Log("Блок Else");
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

    private IEnumerator UpdateProgressBar()
    {
        while (soundPlayer.time < audioDuration)
        {
            if (soundPlayer.clip == clip && isPlay)
            {
                yield return new WaitForEndOfFrame();
                progressBar.fillAmount = (float)soundPlayer.time / audioDuration;
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
}
