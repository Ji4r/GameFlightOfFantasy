using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    [RequireComponent(typeof(Image))]
    public class STElementHistory : MonoBehaviour
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void SetColor(ref Color color)
        {
            image.color = color;

        }       
    }
}
