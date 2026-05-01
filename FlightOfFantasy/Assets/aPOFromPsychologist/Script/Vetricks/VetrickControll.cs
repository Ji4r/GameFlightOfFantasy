using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace DiplomGames
{
    public class VetrickControll : MonoBehaviour
    {
        [SerializeField] private GameObject vetrickObject;
        [SerializeField] private float durationAnims = 0.3f;
        [SerializeField] private Image imageVetrick;
        [SerializeField] private Sprite welcomePosVetrick;
        [SerializeField] private Sprite[] posVetrick;

        public PlayPhrases plyer; 
        
        public bool IsActive { get; private set; }
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
            this.IsActive = isActive;
            this.gameObject.SetActive(isActive);
            plyer.enabled = isActive;
        }

        public async Task HideVetrick()
        {
            await vetrickTrans.DOScale(Vector3.zero, durationAnims).AsyncWaitForCompletion();
            vetrickObject.SetActive(false);
        }

        public async Task ShowVetrick(TypePhrase typePhrase)
        {
            if (typePhrase == TypePhrase.Welcome)
            {
                imageVetrick.sprite = welcomePosVetrick;
            }
            if (typePhrase == TypePhrase.Base)
            {
                imageVetrick.sprite = posVetrick[Random.Range(0, posVetrick.Length)];
            }

            vetrickObject.SetActive(true);
            await vetrickTrans.DOScale(baseScale, durationAnims).AsyncWaitForCompletion();
        }
    }
}
