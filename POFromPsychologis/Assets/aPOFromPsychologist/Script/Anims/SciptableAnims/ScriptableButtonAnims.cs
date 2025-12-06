using DG.Tweening;
using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "ScriptableButtonAnims", menuName = "ScriptableObjects/ButtonAnims")]
    public class ScriptableButtonAnims : ScriptableObject
    {
        [Header("При наведении на кнопку")]
        public float AddSizeOnHover = 0.15f;
        public float durationAnimsOnHover = 0.2f;
        public Ease easeScheduleHover = Ease.OutCubic;

        [Header("При нажатии на кнопку")]
        public float AddSizeOnClick = 0.25f;
        public float durationAnimsOnClick = 0.2f;
        public Ease easeScheduleClick = Ease.OutCubic;
    }
}
