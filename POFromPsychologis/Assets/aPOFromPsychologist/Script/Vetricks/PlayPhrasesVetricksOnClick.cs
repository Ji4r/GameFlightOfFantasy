using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class PlayPhrasesVetricksOnClick : PlayPhrases
    {
        [SerializeField] private Button btn;

        private void OnEnable()
        {
            btn.onClick.AddListener(NextPhrase);
        }

        private void OnDisable()
        {
            btn.onClick.RemoveListener(NextPhrase);
            SoundVetrickVoice.instance.StopCurrentSound();
        }
    }
}
