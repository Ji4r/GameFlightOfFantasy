using UnityEngine;

namespace DiplomGames
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private SlotFindSound MainSlot;
        [SerializeField] private SlotFindSound[] slots;



        public void ReleaseAllSlots()
        {
            MainSlot.DeleteAllChildren();
            foreach (SlotFindSound slot in slots) 
            {
                slot.DeleteAllChildren();
            }
        }
    }

    [System.Serializable]
    public class SlotFindSound
    {
        public Transform slot;
        private bool isFree;

        public bool SlotIsFree() => isFree;

        public void ReleaseTheSlot()
        {
            isFree = true;
        }

        public void FillTheSlot()
        {
            isFree = false;
        }

        public void DeleteAllChildren()
        {
            if (slot.childCount > 0)
            {
                foreach (Transform child in slot)
                {
                    GameObject.Destroy(child);
                }

                isFree = true;
            }
        }
    }
}
