using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class STColorValidator : MonoBehaviour
    {
        [Inject] private STHistoryColor historyColor;
        
        public Action AnErrorWasMade;
        public Action EverythingIsCorrect;

        private List<Color> originalListOfColors;
        private List<Color> listFromInput = new();

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

        public void CleatInputList()
        {
            listFromInput?.Clear();
        }
    }
}
