using DG.Tweening;
using UnityEngine;

namespace DiplomGames
{
    public class AnimsButtonRotate : MonoBehaviour
    {
        [Header("При наведении на кнопку")]
        [SerializeField] private float AddRotateOnHover = 180;
        [SerializeField] private float durationAnimsOnHover = 0.4f;
        [SerializeField] private Ease easeScheduleHover = Ease.OutCubic;

        private Quaternion baseRotate;
        private Transform btnTransform;
        private Tween anims;
        private Quaternion rotateOnHover;

        private void Awake()
        {
            btnTransform = transform;

            baseRotate = btnTransform.localRotation;

            rotateOnHover = Quaternion.Euler(baseRotate.x, baseRotate.y, baseRotate.z + AddRotateOnHover);
        }

        public void OnEnter()
        {
            KillAnims();

            btnTransform.DOLocalRotateQuaternion(rotateOnHover, durationAnimsOnHover).SetEase(easeScheduleHover);
        }

        public void OnExit()
        {
            KillAnims();

            btnTransform.DOLocalRotateQuaternion(baseRotate, durationAnimsOnHover).SetEase(easeScheduleHover);
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
