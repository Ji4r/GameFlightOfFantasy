using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace DiplomGames
{
    public class VetrickControll : MonoBehaviour
    {
        [SerializeField] private GameObject vetrickObject;
        [SerializeField] private float durationAnims = 0.3f;

        public PlayPhrases plyer; 
        
        private bool isActive = false;
        private Vector3 baseScale;
        private Transform vetrickTrans;

        public GameObject VetrickObject { get { return vetrickObject; } }

        private void Start()
        {
            vetrickTrans = vetrickObject.transform;
            baseScale = vetrickObject.transform.localScale;
        }

        public void SetActivity(bool isActive)
        {
            this.isActive = isActive;
            this.gameObject.SetActive(isActive);
            plyer.enabled = isActive;
        }

        public async Task HideVetrick()
        {
            await vetrickTrans.DOScale(Vector3.zero, durationAnims).AsyncWaitForCompletion();
            vetrickObject.SetActive(false);
        }

        public async Task ShowVetrick()
        {
            vetrickObject.SetActive(true);
            await vetrickTrans.DOScale(baseScale, durationAnims).AsyncWaitForCompletion();
        }
    }
}
