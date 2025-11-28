using UnityEngine;
using System.Collections.Generic;

namespace DiplomGames
{
    public class STGenerateColorSubsequnce 
    {
        private int currentComplexity;

        public List<Color> GenerateSubsequnceColor(List<Color> colors, Range range)
        {
            int sebsequnceSize = Random.Range(range.minValue, range.maxValue);
            List<Color> sebsequnceColor = new();
            Color getColor;

            for (int i = 0; i < sebsequnceSize; i++) 
            {
                getColor = colors[Random.Range(0, colors.Count)];
                sebsequnceColor.Add(getColor);
            }

            return sebsequnceColor;
        }
    }
}
