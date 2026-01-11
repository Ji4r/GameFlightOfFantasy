namespace DiplomGames
{
    public class PlayPhrasesVetricksOnCall : PlayPhrases
    {
        public void PlayPhrase()
        {
            if (dialogue == null)
                NextPhrase();
        }
    }
}
