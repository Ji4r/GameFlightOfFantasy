using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class GameHistory : MonoBehaviour
    {
        [System.Serializable]
        public class GameResult
        {
            public float multiplier;
            public bool crashed;
            public float winAmount;
        }

        [SerializeField] private Transform historyContainer;
        [SerializeField] private GameObject historyItemPrefab;
        [SerializeField] private int maxHistoryItems = 10;

        private Queue<GameObject> historyItems = new Queue<GameObject>();
        private List<GameResult> gameResults = new List<GameResult>();

        public void AddResult(float multiplier, bool crashed, float winAmount)
        {
            GameResult result = new GameResult
            {
                multiplier = multiplier,
                crashed = crashed,
                winAmount = winAmount
            };

            gameResults.Add(result);

            // Создаем новый элемент истории
            GameObject newItem = Instantiate(historyItemPrefab, historyContainer);
            HistoryItem item = newItem.GetComponent<HistoryItem>();

            if (item != null)
            {
                item.Setup(multiplier, crashed, winAmount);
            }

            // Добавляем в очередь и ограничиваем количество
            historyItems.Enqueue(newItem);

            if (historyItems.Count > maxHistoryItems)
            {
                GameObject oldItem = historyItems.Dequeue();
                Destroy(oldItem);
            }
        }

        public float GetAverageMultiplier()
        {
            if (gameResults.Count == 0) return 0;

            float sum = 0;
            foreach (var result in gameResults)
            {
                sum += result.multiplier;
            }
            return sum / gameResults.Count;
        }
    }
}
