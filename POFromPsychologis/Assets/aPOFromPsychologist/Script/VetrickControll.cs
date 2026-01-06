using UnityEngine;

namespace DiplomGames
{
    public class VetrickControll : MonoBehaviour
    {
        [SerializeField] private GameObject vetrickObject;
        
        private bool isActive = false;

        public void SetActivity(bool isActive)
        {
           this.isActive = isActive;
            vetrickObject.SetActive(isActive);
        }
    }
}
