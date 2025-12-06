using UnityEngine;
using UnityEngine.SceneManagement;

namespace DiplomGames
{
    public class EnterToScene : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Casino");
            }
        }
    }
}
