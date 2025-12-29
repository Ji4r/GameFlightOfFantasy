using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "PresetColorAnimsWheel", menuName = "ScriptableObjects/SimonSays/ColorAnimsWheel")]
    public class STPresetColorAnimsWheel : ScriptableObject
    {
        [SerializeField, Tooltip("Интервал Между цветами")] private float intervalBetweenColors;
        [SerializeField, Tooltip("Длительность Появления Цвета")] private float durationOfColorAppearance;
        [SerializeField, Tooltip("Длительность Исчезновения Цвета")] private float colorFadeDuration;
        [SerializeField, Tooltip("Длительность Отображения цвета")] private float сolorDisplayDuration;

        public float ColorDisplayDuration { get { return сolorDisplayDuration; } }
        public float IntervalBetweenColors { get { return intervalBetweenColors; } }
        public float ColorFadeDuration { get { return colorFadeDuration; } }
        public float DurationOfColorAppearance { get { return durationOfColorAppearance; } }
    }
}
