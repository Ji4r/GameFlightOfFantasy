using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DiplomGames
{
    public class PlayPhrasesVetricks : MonoBehaviour
    {
        [SerializeField] private PhraseVetrick phrases;

        [SerializeField] private TextMeshProUGUI textVetrick;
        [SerializeField] private Button btn;

        private PhrseAndClip currentPhrase;
        private Coroutine dialogue;

        private Queue<PhrseAndClip> listOfPhrases;

        private void OnEnable()
        {
            btn.onClick.AddListener(ClickOnVetrick);
        }

        private void OnDisable()
        {
            btn.onClick.RemoveListener(ClickOnVetrick);
            SoundPlayer.instance.StopCurrentSound();
        }

        private void Start()
        {
            GenerateListPhrase();
            dialogue = StartCoroutine(StartADialogue(phrases.GetWelcomePhrase()));
        }

        private void ClickOnVetrick()
        {
            if (dialogue != null)
            {
                StopCoroutine(dialogue);
                ClearText();
                textVetrick.text = currentPhrase.vetrikasPhrases;
                SoundPlayer.instance.StopCurrentSound();
                dialogue = null;
                return;
            } // Увидить полный текст при двойном нажатии
            if (listOfPhrases == null || listOfPhrases.Count == 0)
                GenerateListPhrase();

            dialogue = StartCoroutine(StartADialogue(listOfPhrases.Dequeue()));
        }

        private IEnumerator StartADialogue(PhrseAndClip phrase)
        {
            ClearText();
            float delayBetweenCharacters;

            if (phrase.audioClip != null)
            {
                delayBetweenCharacters = phrase.audioClip.length / phrase.vetrikasPhrases.Length;
                SoundPlayer.instance.PlayWithStop(phrase.audioClip);
            }
            else
                delayBetweenCharacters = 0.10f;

            currentPhrase = phrase;
            foreach (var symbol in phrase.vetrikasPhrases) 
            {
                textVetrick.text += symbol;
                yield return new WaitForSeconds(delayBetweenCharacters);
            }

            dialogue = null;
            currentPhrase.vetrikasPhrases = "";
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
            var shuffledList = phrases.GetAllPhrase().OrderBy(x => random.Next()).ToList();

            foreach (var shuffled in shuffledList)
            {
                listOfPhrases.Enqueue(shuffled);
            }
        }
    }
}
