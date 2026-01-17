using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task AddItemInListInput(Color color)
        {
            listFromInput.Add(color);
            await historyColor.AddColorInHistory(color);
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

        private async void CleatLists()
        {
            await historyColor.ClearHistory();
            originalListOfColors?.Clear();
            listFromInput?.Clear();
        }

        public void CleatInputList()
        {
            listFromInput?.Clear();
        }
    }
}
