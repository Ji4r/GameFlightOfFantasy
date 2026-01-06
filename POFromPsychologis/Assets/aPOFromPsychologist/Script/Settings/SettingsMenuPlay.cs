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
            btnReplay.onClick.AddListener(() => { entryPoint.ReloadSceneClearInstance(SwitchScene.GetActiveSceneIndex()); });
            //btnUndergoTraining.onClick.AddListener();
            btnExitToMenu.onClick.AddListener(() => { entryPoint.LoadScene(1); });
            btnExitGame.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            btnReplay.onClick.RemoveListener(SwitchScene.RestartScene);
            //btnUndergoTraining.onClick.RemoveListener();
            btnExitToMenu.onClick.RemoveListener(() => { entryPoint.LoadScene(1); });
            btnExitGame.onClick.RemoveListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
