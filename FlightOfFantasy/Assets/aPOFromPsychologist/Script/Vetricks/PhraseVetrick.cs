using UnityEngine;

namespace DiplomGames
{
    public class PhraseVetrick : MonoBehaviour
    {
        [SerializeField] private string welcomePhrase;
        [SerializeField] private AudioClip welcomeAudio;

        [SerializeField] private PhrseAndClip[] phrases;

        public PhrseAndClip[] GetAllPhrase()
        {
            return phrases;
        }

        public string[] GetAllPhraseOnlyText()
        {
            string[] phrasesText = new string[phrases.Length + 1];
            for (int i = 1; i < phrases.Length; i++)
            {
                phrasesText[i] = phrases[i].vetrikasPhrases;
            }
            phrasesText[0] = welcomePhrase;
            return phrasesText;
        }

        public AudioClip[] GetAllPhraseOnlyClip()
        {
            AudioClip[] phrasesAudio = new AudioClip[phrases.Length + 1];
            for (int i = 1; i < phrases.Length; i++)
            {
                phrasesAudio[i] = phrases[i].audioClip;
            }
            phrasesAudio[0] = welcomeAudio;
            return phrasesAudio;
        }

        public PhrseAndClip GetWelcomePhrase()
        {
            return new PhrseAndClip(welcomePhrase, welcomeAudio);
        }

        public PhrseAndClip GetPhraseByText(string text)
        {
            for (int i = 0; i < phrases.Length; i++)
            {
                if (text == phrases[i].vetrikasPhrases)
                {
                    return phrases[i];
                }
            }

            throw new System.Exception($"Не найден PhrseAndClip по тексту - {text}");
        }
    }

    [System.Serializable]
    public class PhrseAndClip
    {
        public string vetrikasPhrases;
        public AudioClip audioClip;

        public PhrseAndClip() { }
        public PhrseAndClip(string phrase, AudioClip audioClip) 
        {
            vetrikasPhrases = phrase;
            this.audioClip = audioClip;
        }
    }
}
