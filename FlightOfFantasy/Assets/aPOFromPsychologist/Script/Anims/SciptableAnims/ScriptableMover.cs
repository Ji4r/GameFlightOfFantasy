using DG.Tweening;
using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "ScriptableShakeAnims", menuName = "ScriptableObjects/MoverPreset")]
    public class ScriptableMover : ScriptableObject
    {
        [SerializeField] private float durationAnims;
        [SerializeField] private Ease easeMover = Ease.Linear;

        public float DurationAnims => durationAnims;
        public Ease EaseMover => easeMover;
    }
}
