using UnityEngine;

namespace DiplomGames
{
    public enum ListSound
    {
        buttonPointer,
        buttonEnter,
    }

    public class SoundList : MonoBehaviour
    {
        public static SoundList instance { get; private set; }

        public SoundAudioClip[] soundAudioClips;

        private void Awake()
        {
            if (instance == null)
                instance = this;
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
