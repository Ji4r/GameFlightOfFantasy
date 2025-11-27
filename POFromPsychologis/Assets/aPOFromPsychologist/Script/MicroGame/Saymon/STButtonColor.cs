using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STButtonColor : MonoBehaviour
    {
        private Image imageColor;

        private void Start()
        {
            imageColor = GetComponent<Image>();
        }
    }
}
