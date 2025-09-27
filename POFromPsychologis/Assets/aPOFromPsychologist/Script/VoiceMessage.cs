using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VoiceMessage : MonoBehaviour
{
    [SerializeField] private LineRenderer _prefab;
    [SerializeField] private Gradient _activeAudioColor;
    [SerializeField] private Gradient _notActiveAudioColor;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private string url = "";
    private AudioClip _audioClip;
    private float[] amplitudeData;
    private List<LineRenderer> _lines = new();

    private IEnumerator Start()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка загрузки: " + www.error);
            }
            else
            {
                _audioClip = DownloadHandlerAudioClip.GetContent(www);
                _audioSource.clip = _audioClip;
            }
        }

        AnalyzeAudio();

        for (int i = 0; i < 48; i++)
        {
            var a = Instantiate(_prefab, transform);
            _lines.Add(a);
            a.GetComponent<RectTransform>().anchoredPosition = new Vector2(40 + i * 2.5f, 0);
            a.SetPositions(new Vector3[] { new(0, -amplitudeData[i] * 15, 0), new(0, amplitudeData[i] * 15, 0) });
        }
    }

    private void Update()
    {
        if (_audioClip == null) return;

        var time01 = _audioSource.time / _audioSource.clip.length;
        for (int i = 0; i < _lines.Count * time01; i++)
        {
            _lines[i].colorGradient = _activeAudioColor;
        }
    }

    void AnalyzeAudio()
    {
        AudioClip clip = _audioSource.clip;
        int sampleCount = clip.samples;
        int channels = clip.channels;

        float[] samples = new float[sampleCount * channels];
        clip.GetData(samples, 0);

        int segmentSize = sampleCount / 48;
        amplitudeData = new float[48];

        for (int i = 0; i < 48; i++)
        {
            int startIdx = i * segmentSize * channels;
            int endIdx = startIdx + segmentSize * channels;
            float maxAmp = 0f;

            for (int j = startIdx; j < endIdx && j < samples.Length; j += channels)
            {
                for (int ch = 0; ch < channels; ch++)
                {
                    float absSample = Mathf.Abs(samples[j + ch]);
                    if (absSample > maxAmp)
                        maxAmp = absSample;
                }
            }
            amplitudeData[i] = maxAmp;
        }
    }
}
