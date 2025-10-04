using UnityEngine;
using UnityEngine.SceneManagement;

namespace DiplomGames
{
    public class SwitchScene : MonoBehaviour
    {
        public void SwitchSceneById(int id)
        {
            SceneManager.LoadScene(id);
        }
    }
}
