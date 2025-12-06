using DiplomGames;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayManager : MonoBehaviour
{
    [SerializeField] private LuckyJetGame gameController;
    [SerializeField] private Toggle autoPlayToggle;
    [SerializeField] private Slider targetMultiplierSlider;
    [SerializeField] private TextMeshProUGUI targetMultiplierText;
    [SerializeField] private TMP_InputField numberOfGamesInput;
    [SerializeField] private TextMeshProUGUI gamesPlayedText;
    [SerializeField] private TextMeshProUGUI autoPlayStatusText;

    private bool isAutoPlaying = false;
    private int gamesPlayed = 0;
    private int targetGames = 10;
    private Coroutine autoPlayCoroutine;

    void Start()
    {
        autoPlayToggle.onValueChanged.AddListener(OnAutoPlayToggle);
        targetMultiplierSlider.onValueChanged.AddListener(OnTargetMultiplierChanged);

        if (numberOfGamesInput != null)
        {
            numberOfGamesInput.onEndEdit.AddListener(OnNumberOfGamesChanged);
            numberOfGamesInput.text = targetGames.ToString();
        }

        UpdateUI();
    }

    void OnAutoPlayToggle(bool isOn)
    {
        isAutoPlaying = isOn;

        if (isAutoPlaying)
        {
            if (autoPlayCoroutine != null)
                StopCoroutine(autoPlayCoroutine);

            autoPlayCoroutine = StartCoroutine(AutoPlayCoroutine());
        }
        else
        {
            if (autoPlayCoroutine != null)
            {
                StopCoroutine(autoPlayCoroutine);
                autoPlayCoroutine = null;
            }

            autoPlayStatusText.text = "Автоигра остановлена";
            autoPlayStatusText.color = Color.yellow;
        }
    }

    void OnTargetMultiplierChanged(float value)
    {
        targetMultiplierText.text = $"Цель: {value:F1}x";
    }

    void OnNumberOfGamesChanged(string value)
    {
        if (int.TryParse(value, out int result))
        {
            targetGames = Mathf.Clamp(result, 1, 1000);
            UpdateUI();
        }
    }

    IEnumerator AutoPlayCoroutine()
    {
        gamesPlayed = 0;

        autoPlayStatusText.text = "Автоигра запущена...";
        autoPlayStatusText.color = Color.green;

        while (isAutoPlaying && gamesPlayed < targetGames)
        {
            // Проверяем, можно ли начать новую игру
            if (!gameController.IsGameRunning && gameController.CanPlaceBet())
            {
                // Устанавливаем целевой множитель в контроллер игры
                gameController.SetTargetMultiplier(targetMultiplierSlider.value);

                // Запускаем игру
                gameController.StartGame();

                // Ждем окончания игры
                yield return StartCoroutine(WaitForGameCompletion());

                gamesPlayed++;
                UpdateUI();

                // Пауза между играми
                yield return new WaitForSeconds(1f);
            }
            else
            {
                // Если игра активна, ждем
                yield return null;
            }
        }

        // Автоигра завершена
        isAutoPlaying = false;
        autoPlayToggle.isOn = false;

        autoPlayStatusText.text = $"Автоигра завершена! Игр сыграно: {gamesPlayed}";
        autoPlayStatusText.color = Color.cyan;
    }

    IEnumerator WaitForGameCompletion()
    {
        // Ждем, пока игра не завершится
        while (gameController.IsGameRunning)
        {
            yield return null;
        }
    }

    void UpdateUI()
    {
        if (gamesPlayedText != null)
            gamesPlayedText.text = $"Игр сыграно: {gamesPlayed}/{targetGames}";
    }

    // Для остановки при уничтожении объекта
    void OnDestroy()
    {
        if (autoPlayCoroutine != null)
            StopCoroutine(autoPlayCoroutine);
    }

    // Метод для принудительной остановки автоигры
    public void StopAutoPlay()
    {
        isAutoPlaying = false;
        autoPlayToggle.isOn = false;

        if (autoPlayCoroutine != null)
        {
            StopCoroutine(autoPlayCoroutine);
            autoPlayCoroutine = null;
        }

        autoPlayStatusText.text = "Автоигра остановлена вручную";
        autoPlayStatusText.color = Color.red;
    }
}