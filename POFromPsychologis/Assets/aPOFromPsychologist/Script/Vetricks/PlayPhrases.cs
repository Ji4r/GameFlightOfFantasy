using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DiplomGames
{
    public class PlayPhrases : MonoBehaviour
    {
        [SerializeField] protected PhraseVetrick phrases;
        [SerializeField] protected TextMeshProUGUI textVetrick;
        [SerializeField] protected float symbolAppearanceTime = 0.6f;

        protected PhrseAndClip currentPhrase;
        protected Coroutine dialogue;
        protected Queue<PhrseAndClip> listOfPhrases;
        protected System.Random random = new System.Random();


        protected virtual void Start()
        {
            GenerateListPhrase();
            dialogue = StartCoroutine(StartADialogue(phrases.GetWelcomePhrase()));
        }

        protected virtual void NextPhrase()
        {
            if (SkipDialogue())
                return;

            if (listOfPhrases == null || listOfPhrases.Count == 0)
                GenerateListPhrase();

            dialogue = StartCoroutine(StartADialogue(listOfPhrases.Dequeue()));
        }

        protected virtual IEnumerator StartADialogue(PhrseAndClip phrase)
        {
            ClearText();
            float delayBetweenCharacters;

            if (phrase.audioClip != null)
            {
                delayBetweenCharacters = phrase.audioClip.length / phrase.vetrikasPhrases.Length;
                SoundPlayer.instance.PlayWithStop(phrase.audioClip);
            }
            else
                delayBetweenCharacters = symbolAppearanceTime;

            currentPhrase = phrase;
            foreach (var symbol in phrase.vetrikasPhrases)
            {
                textVetrick.text += symbol;
                yield return new WaitForSeconds(delayBetweenCharacters);
            }

            dialogue = null;
            currentPhrase.vetrikasPhrases = "";
        }

        protected virtual void ClearText()
        {
            textVetrick.text = string.Empty;
        }

        protected virtual void GenerateListPhrase()
        {
            if (listOfPhrases == null)
                listOfPhrases = new();

            listOfPhrases.Clear();
            var shuffledList = phrases.GetAllPhrase().OrderBy(x => random.Next()).ToList();

            foreach (var shuffled in shuffledList)
            {
                listOfPhrases.Enqueue(shuffled);
            }
        }

        protected virtual bool SkipDialogue()
        {
            if (dialogue != null)
            {
                StopCoroutine(dialogue);
                ClearText();
                textVetrick.text = currentPhrase.vetrikasPhrases;
                SoundPlayer.instance.StopCurrentSound();
                dialogue = null;
                return true;
            }
            else
                return false;
        }
    }
}
