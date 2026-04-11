using DG.Tweening;
using UnityEngine;

namespace DiplomGames
{
    public class AnimsButtonRotate : MonoBehaviour, IAnimsButton
    {
        [Header("При наведении на кнопку")]
        [SerializeField] private float AddRotateOnHover = 180;
        [SerializeField] private float durationAnimsOnHover = 0.4f;
        [SerializeField] private Ease easeScheduleHover = Ease.OutCubic;
        [SerializeField] private Transform spriteTransform;

        private Quaternion baseRotate;
        private Tween anims;
        private Quaternion rotateOnHover;

        private void Awake()
        {
            baseRotate = spriteTransform.localRotation;

            rotateOnHover = Quaternion.Euler(baseRotate.x, baseRotate.y, baseRotate.z + AddRotateOnHover);
        }

        public void OnEnter()
        {
            KillAnims();

            spriteTransform.DOLocalRotateQuaternion(rotateOnHover, durationAnimsOnHover).SetEase(easeScheduleHover);
        }

        public void OnExit()
        {
            KillAnims();

            spriteTransform.DOLocalRotateQuaternion(baseRotate, durationAnimsOnHover).SetEase(easeScheduleHover);
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

        public void OnDown()
        {  
        }

        public void OnUp()
        {
        }
    }
}
