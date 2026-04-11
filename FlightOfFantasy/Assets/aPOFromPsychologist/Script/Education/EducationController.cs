using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DiplomGames
{
    public class EducationController : MonoBehaviour
    {
        [SerializeField] private ScriptableEducation preset;
        [SerializeField] private TextMeshProUGUI textVetrick;
        [SerializeField] private VideoPlayer player;
        [SerializeField] private float symbolAppearanceTime = 0.06f;
        [SerializeField] private Button nextStep;
        [SerializeField] private Button prevStep;

        private TutorPreset currentPhrase;
        protected Coroutine dialogue;
        private sbyte currentStepTutor;

        private void OnEnable()
        {
            nextStep.onClick.AddListener(NextStep);
            prevStep.onClick.AddListener(PrevStep);
        }

        protected void Start()
        {
            if (preset == null || preset.Tutor == null || preset.Tutor.Length == 0)
            {
                Debug.LogError("Не найден экземпляр типа - ScriptableEducation для поля preset");
                return;
            }

            currentStepTutor = -1;
            NextStep();
        }

        private void OnDisable() 
        {
            nextStep.onClick.RemoveListener(NextStep);
            prevStep.onClick.RemoveListener(PrevStep);
        }


        private void NextStep()
        {
            if (SkipDialogue())
                return;

            var nextStep = currentStepTutor;
            nextStep++;

            if (nextStep > preset.Tutor.Length - 1)
            {
                currentStepTutor = 0;
                NextPhrase();
                return;
            }

            currentStepTutor = nextStep;
            NextPhrase();
        }

        private void PrevStep()
        {
            if (SkipDialogue())
                return;

            var nextStep = currentStepTutor;
            nextStep--;

            if (nextStep < 0)
            {
                currentStepTutor = (sbyte)(preset.Tutor.Length - 1);
                NextPhrase();
                return;
            }

            currentStepTutor = nextStep;
            NextPhrase();
        }

        protected void NextPhrase()
        {
            if (preset.Tutor == null || preset.Tutor.Length == 0)
            {
                Debug.LogError("Массив пуст в preset.Tutor");
                return;
            }

            dialogue = StartCoroutine(StartADialogue(preset.Tutor[currentStepTutor]));
        }

        protected IEnumerator StartADialogue(TutorPreset phrase)
        {
            ClearText();
            float delayBetweenCharacters;

            if (phrase.AudioClip != null)
            {
                delayBetweenCharacters = phrase.AudioClip.length / phrase.Text.Length;
                SoundVetrickVoice.instance.PlayWithStop(phrase.AudioClip);
            }
            else
                delayBetweenCharacters = symbolAppearanceTime;

            player.clip = phrase.VideoClip;
            currentPhrase = phrase;
            foreach (var symbol in phrase.Text)
            {
                textVetrick.text += symbol;
                yield return new WaitForSeconds(delayBetweenCharacters);
            }

            dialogue = null;
        }

        protected void ClearText()
        {
            textVetrick.text = string.Empty;
        }

        protected bool SkipDialogue()
        {
            if (dialogue != null)
            {
                StopCoroutine(dialogue);
                ClearText();
                textVetrick.text = currentPhrase.Text;
                SoundVetrickVoice.instance.StopCurrentSound();
                dialogue = null;
                return true;
            }
            else
                return false;
        }
    }
}
