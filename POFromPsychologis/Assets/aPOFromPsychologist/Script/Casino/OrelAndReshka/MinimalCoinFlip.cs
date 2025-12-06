using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DiplomGames;

public class MinimalCoinFlip : MonoBehaviour
{
    [SerializeField] private Button headsButton;     // Кнопка "Орёл"
    [SerializeField] private Button tailsButton;     // Кнопка "Решка"
    [SerializeField] private TextMeshProUGUI resultText;     // Текст результата игры
    [SerializeField] private TextMeshProUGUI coinResultText; // Текст: что выпало (Орёл/Решка)
    [SerializeField] private TextMeshProUGUI playerChoiceText; // Текст: что выбрал игрок
    [SerializeField] private Balance balance;
    [SerializeField] private SetBet bet;

    private void Start()
    {
        // Привязываем обработчики кликов к кнопкам:
        // При клике на "Орёл" вызываем FlipCoin(true)
        headsButton.onClick.AddListener(() => FlipCoin(true));

        // При клике на "Решка" вызываем FlipCoin(false)
        tailsButton.onClick.AddListener(() => FlipCoin(false));

        // Устанавливаем начальный текст
        resultText.text = "Выберите сторону";
        coinResultText.text = "";
        playerChoiceText.text = "";
    }

    // Основной метод игры - бросок монеты
    void FlipCoin(bool playerChoseHeads)
    {
        if (bet.bet <= 0) return;

        // Обновляем текст выбора игрока
        playerChoiceText.text = $"Вы выбрали: {(playerChoseHeads ? "Орёл" : "Решка")}";

        // Генерируем случайный результат:
        // Random.Range(0, 2) возвращает 0 или 1
        // 0 = Орёл (true), 1 = Решка (false)
        bool isHeads = Random.Range(0, 2) == 0;

        // Показываем результат броска текстом
        coinResultText.text = $"Выпало: {(isHeads ? "ОРЁЛ" : "РЕШКА")}";

        // Проверяем, выиграл ли игрок
        if (playerChoseHeads == isHeads)
        {
            // Если выбор игрока совпал с результатом
            resultText.text = "Вы выиграли!";
            resultText.color = Color.green;
            balance.AddBalance(bet.bet * 2);
        }
        else
        {
            // Если не совпал
            resultText.text = "Вы проиграли!";
            resultText.color = Color.red;
            balance.balance -= bet.bet;
            balance.UpdMoney();
        }
    }
}   