using System;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class MCardProperties : MonoBehaviour
    {
        [SerializeField] private float durationAnimsFlip = 0.3f;

        [Header("Ссылка на стороны карт")]
        [SerializeField, Tooltip("Сторона которая видна всегда")] private Transform frontCardTransform;
        [SerializeField] private Image imageFrontSideCard;
        [SerializeField, Tooltip("Сторона которая видна при открытии")] private Transform backCardTransform;
        [SerializeField] private Image imageBackSideCard;

        private Button btnCard;

        public Button BtnCard => btnCard;
        public bool IsShow { get; private set; }
        public bool IsFind { get; private set; }
        public int UniqueId { get; private set; } = -1;

       
        private MCardAnims mCardAnims;


        private void Awake()
        {
            mCardAnims = new MCardAnims(durationAnimsFlip);
            btnCard = GetComponent<Button>();
        }

        public void SetSpriteOnBackSide(Sprite newSprite)
        {
            imageBackSideCard.sprite = newSprite;
        }

        public void ShowFacialSide(Action callback = null)
        {
            mCardAnims.RotateCard(frontCardTransform, backCardTransform, () =>
            {
                IsShow = true;
                callback?.Invoke();
            });
        }

        public void HideFacialSide()
        {
            IsShow = false;
            mCardAnims.RotateCard(backCardTransform, frontCardTransform);
        }

        public void SetUniqueId(int newId)
        {
            if (UniqueId == -1)
            {
                UniqueId = newId;
            }
        }

        public void FindCard()
        {
            IsFind = true;
            IsShow = false;
            imageBackSideCard.enabled = false;
            imageFrontSideCard.enabled = false;
        }

        private void OnDisable()
        {
            mCardAnims.KillAnims();
        }
    }
}
