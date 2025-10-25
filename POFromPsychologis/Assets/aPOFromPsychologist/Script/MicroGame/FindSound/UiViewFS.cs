using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames 
{ 
    public class UiViewFS : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private Image imageSprite;


        public void ShowOtvetSound(bool onRight)
        {
            if (onRight)
            {
                tmp.color = Color.green;
                tmp.text = "Правильно!";
            }
            else
            {
                tmp.color = Color.red;
                tmp.text = "Ошибка";
            }
        }

        public void ClearShowOtvet()
        {
            tmp.text = string.Empty;
        }

        public void UpdateSpriteProp(Sprite sprite)
        {
            imageSprite.sprite = sprite;
        }
    }
}
