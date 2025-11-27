using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STSimonWheel : MonoBehaviour
    {
        [SerializeField] private Transform parentColorSimon; // Родитель всех цветов

        private STGenerateColorSubsequnce colorSubsequnce;

        private List<Color> GetAllColorWheel() 
        {
            List<Color> allColor = new();

            foreach (Transform t in parentColorSimon) 
            {
                if (t.TryGetComponent<Image>(out var image))
                {
                    allColor.Add(image.color);
                }
            }

            return allColor;
        }

        public void StartSimon(Range range)
        {
            colorSubsequnce = new();
            colorSubsequnce.GenerateSusequnceColor(GetAllColorWheel(), range);
        }
    }
}
