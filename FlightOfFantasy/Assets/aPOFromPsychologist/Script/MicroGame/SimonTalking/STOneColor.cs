using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STOneColor : MonoBehaviour
    {
        [SerializeField] private Image image;
        private Color startColor;
        private Color darkenedColor;

        public void SetStartColor(Color color)
        {
            startColor = color;
        }

        public void SetDarknetColor(Color color)
        {
            darkenedColor = color;
        }

        public void SetImageStartColor()
        {
            image.color = startColor;
        }

        public void SetImageDarknetColor()
        {
            image.color = darkenedColor;
        }
    }
}
