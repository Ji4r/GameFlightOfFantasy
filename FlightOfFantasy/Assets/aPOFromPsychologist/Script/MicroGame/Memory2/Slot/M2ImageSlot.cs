using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class M2ImageSlot : MonoBehaviour
    {
        private Image imageSlot;
        private Transform startSlot;
        private M2SlotChecker slot;

        public Image ImageSlot
        {
            get { return imageSlot;}
            set { imageSlot = value;}   
        }

        private void Awake()
        {
            imageSlot = GetComponent<Image>();
        }

        public void Initialized()
        {
            if (slot == null)
            {
                if (transform.parent.TryGetComponent<M2SlotChecker>(out var slot))
                {
                    this.slot = slot;
                }
            }

            if (startSlot == null)
            {
                startSlot = slot.transform;
            }

            slot.Initialize(transform);
        }
    }
}