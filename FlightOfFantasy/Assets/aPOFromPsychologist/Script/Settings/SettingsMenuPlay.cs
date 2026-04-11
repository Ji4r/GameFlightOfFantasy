using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class SettingsMenuPlay : MonoBehaviour
    {
        [SerializeField] private Button btnReplay;
        [SerializeField] private Button btnUndergoTraining; 
        [SerializeField] private Button btnExitToMenu;
        [SerializeField] private Button btnExitGame;

        [Inject] private EntryPoint entryPoint;

        private void OnEnable()
        {
            btnReplay.onClick.AddListener(ReloadScene);
            //btnUndergoTraining.onClick.AddListener();
            btnExitToMenu.onClick.AddListener(LoadSceneMenu);
            btnExitGame.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            btnReplay.onClick.RemoveListener(ReloadScene);
            //btnUndergoTraining.onClick.RemoveListener();
            btnExitToMenu.onClick.RemoveListener(LoadSceneMenu);
            btnExitGame.onClick.RemoveListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void ReloadScene() 
        {
            entryPoint.ReloadSceneClearInstance(SwitchScene.GetActiveSceneIndex());
        }

        private void LoadSceneMenu()
        {
            entryPoint.LoadScene(1);
        }
    }
}
