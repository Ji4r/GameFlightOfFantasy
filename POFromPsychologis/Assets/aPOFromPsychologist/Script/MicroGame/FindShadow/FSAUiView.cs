using TMPro;
using UnityEngine;

namespace DiplomGames
{
    public class FSAUiView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI answerText;

        public void ShowAnswerText(bool onRight)
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

        public void ClearAnswer()
        {
            answerText.text = string.Empty;
        }

        public void UpdateSpriteProp(Sprite newSprite)
        {

        }
    }
}
