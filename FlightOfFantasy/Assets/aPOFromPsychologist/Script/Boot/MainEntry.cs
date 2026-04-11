using UnityEngine;
using UnityEngine.SceneManagement;

namespace DiplomGames
{
    public class MainEntry
    {
        private static MainEntry instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStart()
        {
            instance = new MainEntry();
            //instance.RunGame();
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            SceneManager.LoadScene(0);
            Debug.ClearDeveloperConsole();
#endif
        }

        public MainEntry()
        {

        }
    }
}
