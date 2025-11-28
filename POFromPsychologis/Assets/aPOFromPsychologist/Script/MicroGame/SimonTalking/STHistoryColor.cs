using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class STHistoryColor : MonoBehaviour
    {
        [Header("Настройки пула")]
        [SerializeField] private STElementHistory prefabHistoryElement;
        [SerializeField] private int sizePool;
        [SerializeField] private Transform containerElement;
        [SerializeField] private bool autoExpend;

        [Header("Анимация")]
        [SerializeField] private float durationAnims;

        private STObjectPool<STElementHistory> objectPool;
        private List<STElementHistory> history;
        private STAnimsHistoryElement anims;

        private void Awake()
        {
            objectPool = new STObjectPool<STElementHistory>(prefabHistoryElement, sizePool, containerElement, autoExpend);
            history = new List<STElementHistory>();
            anims = new STAnimsHistoryElement(durationAnims);
        }

        public void AddColorInHistory(ref Color newColor)
        {
            history.Add(objectPool.GetFreeElement());
            anims.ShowCardElement(history[history.Count - 1].gameObject);
            history[history.Count - 1].SetColor(ref newColor);
        }

        public void ClearHistory()
        {
            var gameObjects = history.ConvertAll(x => x.gameObject);
            anims.HideAllElement(gameObjects, () => 
            {
                Invoke(nameof(DisableAllElemnt), 0.1f);
            });
        }

        private void DisableAllElemnt()
        {
            var gameObjects = history.ConvertAll(x => x.gameObject);
            foreach (var obj in gameObjects)
            {
                DisableElemnt(obj);
            }

            history.Clear();
        }

        private void DisableElemnt(GameObject element)
        {
            element.gameObject.SetActive(false);
            
            if (element.TryGetComponent<STElementHistory>(out var elementHistory))
                history.Remove(elementHistory);
        }

        /// <summary>
        /// Выключает последний элемент истории
        /// </summary>
        private void DisableElemnt()
        {
            history[history.Count - 1].gameObject.SetActive(false);
            history.RemoveAt(history.Count - 1);
        }
    }
}
