using System.Collections;
using TMPro;
using UnityEngine;

namespace DiplomGames
{
    public class M2UiView : MonoBehaviour
    {
        [Header("Текст победы")]
        [SerializeField] private TextMeshProUGUI txtIsWin;
        [SerializeField] private float durationShowTxtIsWin = 3f;
        [SerializeField] private string messageAskSuccesfull = "В точку!";
        [SerializeField] private string messageAskDontSuccesfull = "Не правильно";

        [Header("Окна")]
        [SerializeField] private GameObject panelEndGame;
        [SerializeField] private GameObject panelSelectDiffecalty;

        private Coroutine displayTextCoroutine;

        public void SetEnabledPanelEndGame(bool isActive)
        {
            panelEndGame.SetActive(isActive);
        }
        public void SetEnabledPanelSelectDiffecalty(bool isActive)
        {
            panelSelectDiffecalty.SetActive(isActive);
        }

        public void ShowTxtIsAskSuccesfull()
        {
            if (displayTextCoroutine != null)
            {
                StopCoroutine(displayTextCoroutine);
                displayTextCoroutine = null;
            }

            displayTextCoroutine = StartCoroutine(DisplayTextSuccesfull());
        }

        public void ShowTxtIsAskDontSuccesfull()
        {
            if (displayTextCoroutine != null)
            {
                StopCoroutine(displayTextCoroutine);
                displayTextCoroutine = null;
            }

            displayTextCoroutine = StartCoroutine(DisplayTextDontSuccesfull());
        }

        private IEnumerator DisplayTextSuccesfull()
        {
            txtIsWin.color = Color.green;
            txtIsWin.text = messageAskSuccesfull;

            yield return new WaitForSeconds(durationShowTxtIsWin);

            txtIsWin.text = string.Empty;
            displayTextCoroutine = null;
        }

        private IEnumerator DisplayTextDontSuccesfull()
        {
            txtIsWin.color = Color.red;
            txtIsWin.text = messageAskDontSuccesfull;

            yield return new WaitForSeconds(durationShowTxtIsWin);

            txtIsWin.text = string.Empty;
            displayTextCoroutine = null;
        }
    }
}