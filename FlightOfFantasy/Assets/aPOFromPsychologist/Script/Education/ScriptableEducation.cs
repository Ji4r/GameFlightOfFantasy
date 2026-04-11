using UnityEngine;
using UnityEngine.Video;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "Tutor", menuName = "ScriptableObjects/Tutor/TutorPreset")]
    public class ScriptableEducation : ScriptableObject
    {
        [SerializeField] private TutorPreset[] tutor;
        public TutorPreset[] Tutor { get { return tutor; } }
    }

    [System.Serializable]
    public class TutorPreset
    {
        public string Text;
        public AudioClip AudioClip;
        public VideoClip VideoClip;
    }
}
