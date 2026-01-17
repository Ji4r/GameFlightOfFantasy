using UnityEngine;

namespace DiplomGames
{
    public class VetrickControll : MonoBehaviour
    {
        [SerializeField] private GameObject vetrickObject;
        public PlayPhrases plyer; 
        private bool isActive = false;

        public GameObject VetrickObject { get { return vetrickObject; } }

        public void SetActivity(bool isActive)
        {
            this.isActive = isActive;
            this.gameObject.SetActive(isActive);
            plyer.enabled = isActive;
        }

        public void HideVetrick()
        {
            vetrickObject.SetActive(false);
        }

        public void ShowVetrick()
        {
            vetrickObject.SetActive(true);
        }
    }
}
