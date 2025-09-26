using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundPayerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private Image progressBar;

    private bool isPlay;
    private float audioDuration;
    private Coroutine coroutineBar;

    private void Start()
    {
        isPlay = false;
        audioDuration = clip.length;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isPlay = !isPlay;
        Debug.Log("Click");

        if (soundPlayer.clip == clip)
        {
            if (isPlay)
            {
                Debug.Log("Блок 2 if");
                soundPlayer.Play();
                coroutineBar = StartCoroutine(UpdateProgressBar());             
            }
            else
            {
                Debug.Log("Блок 2 Else");
                soundPlayer.Stop();
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
            Debug.Log($"Длительность трека - {audioDuration} текущая позиция - {soundPlayer.time}");
            Debug.Log($"Попал в блок {clip}");
            Debug.Log($"soundPlayer.clip == clip {soundPlayer.clip == clip && isPlay}");

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
    }
}
