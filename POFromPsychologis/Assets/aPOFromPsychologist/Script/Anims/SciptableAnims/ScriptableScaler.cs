using DG.Tweening;
using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "ScriptableScalerAnims", menuName = "ScriptableObjects/ScalerPreset")]
    public class ScriptableScaler : ScriptableObject
    {
        [SerializeField] private float durationAnims;
        [SerializeField] private Ease easeShake = Ease.OutCubic;

        public float DurationAnims => durationAnims;
        public Ease EaseShake => easeShake;
    }
}
