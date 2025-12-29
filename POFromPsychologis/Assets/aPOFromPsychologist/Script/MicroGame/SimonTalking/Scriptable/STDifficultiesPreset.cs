using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "STDifficultiesPreset", menuName = "ScriptableObjects/SimonSays/RangeDifficulties")]
    public class STDifficultiesPreset : ScriptableObject
    {
        [SerializeField] private Range rangeDifficulties;

        public Range RangeDifficulties { get { return rangeDifficulties; } }
    }
}
