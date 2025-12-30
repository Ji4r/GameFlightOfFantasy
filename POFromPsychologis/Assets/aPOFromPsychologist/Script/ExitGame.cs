using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class ExitGame : MonoBehaviour
    {
        [SerializeField] private Button btnExit;

        private void OnEnable()
        {
            btnExit.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            btnExit.onClick.RemoveAllListeners();
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
