using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class STColorValidator : MonoBehaviour
    {
        [SerializeField, Inject] private STHistoryColor historyColor;
        
        public Action AnErrorWasMade;
        public Action EverythingIsCorrect;

        private List<Color> originalListOfColors;
        private List<Color> listFromInput = new();
        [Inject] private STUiView sTUiView;
        private void Initialize()
        {
            Debug.Log($"HistoryColor: {historyColor != null}");
            Debug.Log($"sTUiView: {sTUiView != null} Ô‡‡‡");
        }

        public void NewSubsequnce(List<Color> newSubsequnce)
        {
            CleatLists();
            originalListOfColors = new List<Color>();
            originalListOfColors.AddRange(newSubsequnce);
        }

        public void AddItemInListInput(ref Color color)
        {
            listFromInput.Add(color);
            historyColor.AddColorInHistory(ref color);
            CheckingMatches();
        }

        private void CheckingMatches()
        {
            var lastIndex = listFromInput.Count - 1;

            if (listFromInput[lastIndex] != originalListOfColors[lastIndex])
            {
                AnErrorWasMade?.Invoke();
                return;
            }

            if (originalListOfColors.Count <= listFromInput.Count)
                EverythingIsCorrect?.Invoke();
        }

        private void CleatLists()
        {
            historyColor.ClearHistory();
            originalListOfColors?.Clear();
            listFromInput?.Clear();
        }
    }
}
