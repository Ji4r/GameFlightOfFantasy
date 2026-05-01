using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class TwoSliderLevelComplexity : MonoBehaviour
    {
        [Header("=== ПЕРВЫЙ СЛАЙДЕР ===")]
        [SerializeField] private TextMeshProUGUI displayValueText;
        [SerializeField] private Slider sliderLevelComlexity;
        [SerializeField] private Button btnAcceptComplexity;

        [SerializeField] private string textWithValue;
        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;
        [SerializeField] private int step;
        [SerializeField] private bool supportsIsInfinity;
        [SerializeField] private bool useEventForMaxValue;

        private LevelComplexity currentLevelComplexity;
        public event Action<LevelComplexity> OnValueChanged;


        [Header("=== ВТОРОЙ СЛАЙДЕР ===")]
        [SerializeField] private TextMeshProUGUI displayValueTextTwo;
        [SerializeField] private Slider sliderLevelComlexityTwo;

        [SerializeField] private string textWithValueTwo;
        [SerializeField] private int minValueTwo;
        [SerializeField] private int maxValueTwo;
        [SerializeField] private int stepTwo;
        [SerializeField] private bool supportsIsInfinityTwo;
        [SerializeField] private bool useEventForMaxValueTwo;

        private LevelComplexity currentLevelComplexityTwo;
        public event Action<LevelComplexity> OnValueChangedTwo;


        private void Start()
        {
            // Первый
            sliderLevelComlexity.minValue = minValue;
            sliderLevelComlexity.maxValue = supportsIsInfinity
                ? maxValue + step
                : maxValue;

            // Второй
            sliderLevelComlexityTwo.minValue = minValueTwo;
            sliderLevelComlexityTwo.maxValue = supportsIsInfinityTwo
                ? maxValueTwo + stepTwo
                : maxValueTwo;
        }


        private void OnEnable()
        {
            // Первый
            btnAcceptComplexity.onClick.AddListener(AcceptAll);
            sliderLevelComlexity.onValueChanged.AddListener(DrawValueInText);

            // Второй
            sliderLevelComlexityTwo.onValueChanged.AddListener(DrawValueInTextTwo);
        }

        private void OnDisable()
        {
            // Первый
            btnAcceptComplexity.onClick.RemoveListener(AcceptAll);
            sliderLevelComlexity.onValueChanged.RemoveListener(DrawValueInText);

            // Второй
            sliderLevelComlexityTwo.onValueChanged.RemoveListener(DrawValueInTextTwo);
        }

        private void AcceptAll()
        {
            AcceptComplexity();
            AcceptComplexityTwo();
        }

        // ===================== ПЕРВЫЙ =====================

        private void DrawValueInText(float value)
        {
            value = Mathf.Round(value / step) * step;

            displayValueText.text = (value > maxValue ? "∞" : value.ToString())
                + " " + textWithValue;
        }

        private void AcceptComplexity()
        {
            float value = Mathf.Round(sliderLevelComlexity.value / step) * step;

            currentLevelComplexity = new LevelComplexity(
                (int)value,
                value > maxValue
            );

            OnValueChanged?.Invoke(currentLevelComplexity);
        }

        public void SetMaxLevelComplexity(int value)
        {
            if (!useEventForMaxValue || value < 0)
                return;

            maxValue = value;

            sliderLevelComlexity.maxValue = supportsIsInfinity
                ? maxValue + step
                : maxValue;
        }


        // ===================== ВТОРОЙ =====================

        private void DrawValueInTextTwo(float value)
        {
            value = Mathf.Round(value / stepTwo) * stepTwo;

            displayValueTextTwo.text = (value > maxValueTwo ? "∞" : value.ToString())
                + " " + textWithValueTwo;
        }

        private void AcceptComplexityTwo()
        {
            float value = Mathf.Round(sliderLevelComlexityTwo.value / stepTwo) * stepTwo;

            currentLevelComplexityTwo = new LevelComplexity(
                (int)value,
                value > maxValueTwo
            );

            OnValueChangedTwo?.Invoke(currentLevelComplexityTwo);
        }

        public void SetMaxLevelComplexityTwo(int value)
        {
            if (!useEventForMaxValueTwo || value < 0)
                return;

            maxValueTwo = value;

            sliderLevelComlexityTwo.maxValue = supportsIsInfinityTwo
                ? maxValueTwo + stepTwo
                : maxValueTwo;
        }
    }
}