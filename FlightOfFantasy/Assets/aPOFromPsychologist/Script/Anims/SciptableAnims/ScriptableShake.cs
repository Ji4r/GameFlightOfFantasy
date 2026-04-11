using DG.Tweening;
using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "ScriptableShakeAnims", menuName = "ScriptableObjects/ShakePreset")]
    public class ScriptableShake : ScriptableObject
    {
        [SerializeField] private float durationAnims;
        [SerializeField] private float powerShake;
        [SerializeField] private Ease easeShake = Ease.OutCubic;

        public float DurationAnims => durationAnims;
        public float PowerShake => powerShake;

        public Ease EaseShake => easeShake;
    }
}
