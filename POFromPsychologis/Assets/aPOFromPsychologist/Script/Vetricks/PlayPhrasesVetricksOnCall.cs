using UnityEngine;
using System.Collections;
using System;

namespace DiplomGames
{
    public class PlayPhrasesVetricksOnCall : PlayPhrases
    {
        [SerializeField] private VetrickControll vetrickController;
        [SerializeField] private float hideDelay = 2f;

        [SerializeField, Tooltip("Похвальные фразы")] private PhraseVetrick MotivationalPhrase;

        private Coroutine currentShutdownVetrick;

        protected override void Start()
        {
            GenerateListPhrase();
        }

        public void PlayWelcomePhrase()
        {
            if (dialogue == null)
                dialogue = StartCoroutine(StartADialogue(phrases.GetWelcomePhrase()));
        }

        public void PlayPhrase()
        {
            if (dialogue == null)
                NextPhrase();
        }

        public void PlayPhraseAndHideVetrick()
        {
            if (dialogue == null)
            {
                if (currentShutdownVetrick != null)
                {
                    StopCoroutine(currentShutdownVetrick);
                    currentShutdownVetrick = null;
                }

                NextPhrase(() =>
                {
                    currentShutdownVetrick = StartCoroutine(HideVetrick());
                });
            }
        }

        private IEnumerator HideVetrick()
        {
            yield return new WaitForSeconds(hideDelay);

            vetrickController.HideVetrick();
            currentShutdownVetrick = null;
        }

        private void NextPhrase(Action callback)
        {
            if (SkipDialogue())
                return;

            if (!vetrickController.VetrickObject.activeInHierarchy)
                vetrickController.ShowVetrick();

            if (listOfPhrases == null || listOfPhrases.Count == 0)
                GenerateListPhrase();

            dialogue = StartCoroutine(StartADialogue(listOfPhrases.Dequeue(), callback));
        }

        protected IEnumerator StartADialogue(PhrseAndClip phrase, Action callback)
        {
            ClearText();
            float delayBetweenCharacters;

            if (phrase.audioClip != null)
            {
                delayBetweenCharacters = phrase.audioClip.length / phrase.vetrikasPhrases.Length;
                SoundVetrickVoice.instance.PlayWithStop(phrase.audioClip);
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
            callback?.Invoke();
        }
    }
}
