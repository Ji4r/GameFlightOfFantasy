using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames 
{ 
    public class UiViewFS : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI answerText;
        [SerializeField] private Image imageSprite;

        public void ShowAnswerSound(bool onRight)
        {
            if (onRight)
            {
                answerText.color = Color.green;
                answerText.text = "Правильно!";
            }
            else
            {
                answerText.color = Color.red;
                answerText.text = "Ошибка";
            }
        }

        public void ClearShowOtvet()
        {
            answerText.text = string.Empty;
        }

        public void UpdateSpriteProp(Sprite sprite)
        {
            imageSprite.sprite = sprite;
        }
    }
}
