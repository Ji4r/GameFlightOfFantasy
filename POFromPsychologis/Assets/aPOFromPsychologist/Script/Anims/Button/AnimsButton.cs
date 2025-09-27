using DG.Tweening;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    [RequireComponent(typeof(Button))]
    public class AnimsButton : MonoBehaviour
    {
        [Header("При наведении на кнопку")]
        [SerializeField] private float AddSizeOnHover = 0.15f;
        [SerializeField] private float durationAnimsOnHover = 0.2f;
        [SerializeField] private Ease easeScheduleHover = Ease.OutCubic;

        [Header("При нажатии на кнопку")]
        [SerializeField] private float AddSizeOnClick = 0.25f;
        [SerializeField] private float durationAnimsOnClick = 0.2f;
        [SerializeField] private Ease easeScheduleClick = Ease.OutCubic;

        private Vector3 baseSize;
        private Button button;
        private Transform btnTransform;
        private Tween anims;

        private void Awake()
        {
            button = GetComponent<Button>();
            btnTransform = transform;

            baseSize = btnTransform.localScale;
        }

        public void OnEnter()
        {
            KillAnims();

            var newSize = new Vector3(baseSize.x + AddSizeOnHover, baseSize.y + AddSizeOnHover, baseSize.z);
            btnTransform.DOScale(newSize, durationAnimsOnHover).SetEase(easeScheduleHover);
        }

        public void OnExit()
        {
            KillAnims();

            btnTransform.DOScale(baseSize, durationAnimsOnHover).SetEase(easeScheduleHover);
        }

        public void OnDown()
        {
            KillAnims();

            var newSize = new Vector3(baseSize.x + AddSizeOnClick, baseSize.y + AddSizeOnClick, baseSize.z);
            btnTransform.DOScale(newSize, durationAnimsOnHover).SetEase(easeScheduleHover);
        }

        public void OnUp()
        {
            KillAnims();

            btnTransform.DOScale(baseSize, durationAnimsOnHover).SetEase(easeScheduleHover);
        }


        private void KillAnims()
        {
            if (anims != null)
                anims.Kill();
        }

        private void OnDisable()
        {
            KillAnims();
        }
    }
}
