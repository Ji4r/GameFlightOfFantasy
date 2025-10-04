using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class ControlVetrickMenu : MonoBehaviour
    {
        [SerializeField] private string welcomePhrase;
        [SerializeField] private string[] vetrikasPhrases;
        [SerializeField] private float delayBetweenCharacters = 0.15f;
        [SerializeField] private TextMeshProUGUI textVetrick;
        [SerializeField] private Button btn;

        private string currentPhrase;
        private Coroutine dialogue;

        private void OnEnable()
        {
            btn.onClick.AddListener(ClickOnVetrick);
        }

        private void OnDisable()
        {
            btn.onClick.RemoveListener(ClickOnVetrick);
        }

        private void Start()
        {
            dialogue = StartCoroutine(StartADialogue(welcomePhrase));
        }

        private void ClickOnVetrick()
        {
            if (dialogue != null)
            {
                StopCoroutine(dialogue);
                ClearText();
                textVetrick.text = currentPhrase;
                dialogue = null;
                return;
            }

            dialogue = StartCoroutine(StartADialogue(vetrikasPhrases[Random.Range(0, vetrikasPhrases.Length)]));
        }

        private IEnumerator StartADialogue(string phrase)
        {
            ClearText();
            currentPhrase = phrase;
            foreach (var symbol in phrase) 
            {
                textVetrick.text += symbol;
                yield return new WaitForSeconds(delayBetweenCharacters);
            }

            dialogue = null;
            currentPhrase = "";
        }

        private void ClearText()
        {
            textVetrick.text = string.Empty;
        }
    }
}
