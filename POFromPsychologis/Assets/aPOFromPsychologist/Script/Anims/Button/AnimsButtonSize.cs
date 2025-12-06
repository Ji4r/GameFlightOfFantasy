using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class AnimsButtonSize : MonoBehaviour, IAnimsButton
    {
        [SerializeField] private ScriptableButtonAnims presetAnims;

        private Vector3 baseSize;
        private Transform btnTransform;
        private Tween anims;
        private Vector3 sizeOnHover;
        private Vector3 sizeOnClick;

        private void Awake()
        {
            btnTransform = transform;

            baseSize = btnTransform.localScale;

            sizeOnHover = new Vector3(baseSize.x + presetAnims.AddSizeOnHover, baseSize.y + presetAnims.AddSizeOnHover, baseSize.z);
            sizeOnClick = new Vector3(baseSize.x + presetAnims.AddSizeOnClick, baseSize.y + presetAnims.AddSizeOnHover, baseSize.z);
        }

        public void OnEnter()
        {
            KillAnims();
  
            btnTransform.DOScale(sizeOnHover, presetAnims.durationAnimsOnHover).SetEase(presetAnims.easeScheduleHover);
        }

        public void OnExit()
        {
            KillAnims();

            btnTransform.DOScale(baseSize, presetAnims.durationAnimsOnHover).SetEase(presetAnims.easeScheduleHover);
        }

        public void OnDown()
        {
            KillAnims();

            btnTransform.DOScale(sizeOnClick, presetAnims.durationAnimsOnHover).SetEase(presetAnims.easeScheduleHover);
        }

        public void OnUp()
        {
            KillAnims();

            btnTransform.DOScale(sizeOnHover, presetAnims.durationAnimsOnHover).SetEase(presetAnims.easeScheduleHover);
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
