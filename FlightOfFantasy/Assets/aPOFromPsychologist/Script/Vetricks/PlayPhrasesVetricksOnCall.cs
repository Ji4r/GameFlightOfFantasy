using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomGames
{
    public enum TypePhrase
    {
        Base, MotivationalPhrase, Welcome
    }

    public class PlayPhrasesVetricksOnCall : PlayPhrases
    {
        [SerializeField] private float hideDelay = 2f;
        [SerializeField] private float hideDelayForWelcome = 10f;
        [SerializeField] private int baseChance;

        [SerializeField, Tooltip("Похвальные фразы")] private PhraseVetrick MotivationalPhrase;

        [Inject] private VetrickControll vetrickControll;

        protected Queue<PhrseAndClip> listOfMotivationalPhrase;

        private Coroutine currentShutdownVetrick;

        protected override void Start()
        {
            GenerateListPhrase();
        }

        public void PlayWelcomePhrase()
        {
            if (dialogue == null && vetrickControll.IsActive)
            {
                dialogue = StartCoroutine(StartADialogue(phrases.GetWelcomePhrase(), () =>
                {
                    currentShutdownVetrick = StartCoroutine(HideVetrick(hideDelayForWelcome));
                }));
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chance">Передйте шанс до 100</param>
        /// <returns></returns>
        public bool ShouldPlayPhrase(int chance = -1)
        {
            if (chance == -1)
            {
                if (baseChance == 100) return true;
                if (baseChance <= 0) return false;

                int randomValue = UnityEngine.Random.Range(0, 100);
                return randomValue < baseChance;
            }
            else
            {
                if (chance == 100) return true;
                if (chance <= 0) return false;

                int randomValue = UnityEngine.Random.Range(0, 100);
                return randomValue < chance;
            }
        }

        public void PlayPhrase()
        {
            if (dialogue == null)
                NextPhrase();
        }

        public async Task PlayPhraseAndHideVetrick(TypePhrase type = TypePhrase.Base)
        {
            if (dialogue == null)
            {
                if (currentShutdownVetrick != null)
                {
                    StopCoroutine(currentShutdownVetrick);
                    currentShutdownVetrick = null;
                }

                await NextPhrase(() =>
                {
                    currentShutdownVetrick = StartCoroutine(HideVetrick(hideDelay));
                }, type);
            }
        }

        private async Task NextPhrase(Action callback, TypePhrase type = TypePhrase.Base)
        {
            if (SkipDialogue())
                return;

            if (!vetrickController.VetrickObject.activeInHierarchy)
            {
                ClearText();
                await vetrickController.ShowVetrick(type);
            }

            if (type == TypePhrase.Base)
            {
                if (listOfPhrases == null || listOfPhrases.Count == 0)
                    GenerateListPhrase();

                dialogue = StartCoroutine(StartADialogue(listOfPhrases.Dequeue(), callback));
            }
            else if (type == TypePhrase.MotivationalPhrase)
            {
                if (listOfMotivationalPhrase == null || listOfMotivationalPhrase.Count == 0)
                    GenerateListPhraseMotivation();

                dialogue = StartCoroutine(StartADialogue(listOfMotivationalPhrase.Dequeue(), callback));
            }
        }


        private IEnumerator HideVetrick(float time)
        {
            yield return new WaitForSeconds(time);

            yield return vetrickController.HideVetrick();
            currentShutdownVetrick = null;
        }
        private IEnumerator StartADialogue(PhrseAndClip phrase, Action callback)
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

        private void GenerateListPhraseMotivation()
        {
            if (listOfMotivationalPhrase == null)
                listOfMotivationalPhrase = new();

            listOfPhrases.Clear();
            var shuffledList = MotivationalPhrase.GetAllPhrase().OrderBy(x => random.Next()).ToList();

            foreach (var shuffled in shuffledList)
            {
                listOfMotivationalPhrase.Enqueue(shuffled);
            }
        }
    }
}
