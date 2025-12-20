using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

        private Queue<string> listOfPhrases;

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
            GenerateListPhrase();
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
            } // Увидить полный текст при двойном нажатии
            if (listOfPhrases == null || listOfPhrases.Count == 0)
                GenerateListPhrase();

            dialogue = StartCoroutine(StartADialogue(listOfPhrases.Dequeue()));
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

        private void GenerateListPhrase()
        {
            if (listOfPhrases == null)
                listOfPhrases = new();

            listOfPhrases.Clear();
            var random = new System.Random();
            var shuffledList = vetrikasPhrases.OrderBy(x => random.Next()).ToList();

            foreach (var shuffled in shuffledList)
            {
                listOfPhrases.Enqueue(shuffled);
            }
        }
    }
}
