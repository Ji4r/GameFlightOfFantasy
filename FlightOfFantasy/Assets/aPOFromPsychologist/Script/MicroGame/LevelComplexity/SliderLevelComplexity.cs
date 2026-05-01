using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class SliderLevelComplexity : MonoBehaviour
    {
        [Header("Ui")]
        [SerializeField] private TextMeshProUGUI displayValueText;
        [SerializeField] private Slider sliderLevelComlexity;
        [SerializeField] private Button btnAcceptComplexity;

        [Header("Данные")]
        [SerializeField, Tooltip("Какой текст рядом с сложностью")] 
        private string textWithValue;

        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;
        [SerializeField] private int step;
        [SerializeField, Tooltip("Может ли быть игра в бесконечном режиме")] 
        private bool supportsIsInfinity;

        [Header("Опционально")]
        [SerializeField] private bool useEventForMaxValue;
               
        private LevelComplexity currentLevelComplexity;
   
        public event Action<LevelComplexity> AcceptComplexityChanged;


        private void Awake()
        {
            if (sliderLevelComlexity == null)
                Debug.Log($"null is sliderLevelComlexity");

            sliderLevelComlexity.minValue = minValue;
            if (supportsIsInfinity)
                sliderLevelComlexity.maxValue = maxValue + step;
            else
                sliderLevelComlexity.maxValue = maxValue;

            displayValueText.text = minValue > maxValue ? "∞" : minValue.ToString();
            displayValueText.text += " " + textWithValue;

            if (!useEventForMaxValue)
            {
                sliderLevelComlexity.value = maxValue / 2 >= minValue ? (int)maxValue / 2 : minValue;
                DrawValueInText(sliderLevelComlexity.value);
            }

            Debug.Log(sliderLevelComlexity.value + " - awake");
        }


        private void OnEnable()
        {
            btnAcceptComplexity.onClick.AddListener(AcceptComplexity);
            sliderLevelComlexity.onValueChanged.AddListener(DrawValueInText);
        }

        private void OnDisable()
        {
            btnAcceptComplexity.onClick.RemoveListener(AcceptComplexity);
            sliderLevelComlexity.onValueChanged.RemoveListener(DrawValueInText);
        }
        public void SetMaxLevelComplexity(int value)
        {
            if (!useEventForMaxValue || value < 0)
                return;

            maxValue = value;
            if (supportsIsInfinity)
                sliderLevelComlexity.maxValue = maxValue + step;
            else
                sliderLevelComlexity.maxValue = maxValue;

            sliderLevelComlexity.value = maxValue / 2 >= minValue ? (int)maxValue / 2 : minValue;
            Debug.Log(sliderLevelComlexity.value + " - SetMax");
        }

        private void DrawValueInText(float value)
        {
            value = Mathf.Round(value / step) * step;
            displayValueText.text = value > maxValue ? "∞" : value.ToString();
            displayValueText.text += " " + textWithValue;
        }
         
        private void AcceptComplexity()
        {
            float value = value = Mathf.Round(sliderLevelComlexity.value / step) * step;

            currentLevelComplexity = new LevelComplexity((int)value, 
                value > maxValue? true : false);

            AcceptComplexityChanged?.Invoke(currentLevelComplexity);
        }
    }

    public struct LevelComplexity 
    {
        public int CurrentLevelComplexity;
        public bool Infinity;

        public LevelComplexity(int currentLevelComplexity, bool infinity)
        {
            this.CurrentLevelComplexity = currentLevelComplexity;
            this.Infinity = infinity;
        }
    }
}
