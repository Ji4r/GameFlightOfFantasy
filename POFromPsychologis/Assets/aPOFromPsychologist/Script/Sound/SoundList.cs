using UnityEngine;

namespace DiplomGames
{
    public enum ListSound
    {
        buttonClick,
        buttonEnter,
        answerSuccesful,
        answerNotSuccesful
    }

    public class SoundList : MonoBehaviour
    {
        public static SoundList instance { get; private set; }

        public SoundAudioClip[] soundAudioClips;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this);
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public ListSound sound;
        public AudioClip audioClip;
    }
}
