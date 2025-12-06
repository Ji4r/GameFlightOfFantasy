using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HistoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private Image background;

    public void Setup(float multiplier, bool crashed, float winAmount)
    {
        multiplierText.text = $"{multiplier:F2}x";

        if (crashed)
        {
            resultText.text = " –¿ÿ";
            resultText.color = Color.red;
            background.color = new Color(1f, 0.8f, 0.8f);
        }
        else
        {
            resultText.text = "«¿¡–¿À";
            resultText.color = Color.green;
            background.color = new Color(0.8f, 1f, 0.8f);
        }

        winText.text = $"${winAmount:F2}";
    }
}