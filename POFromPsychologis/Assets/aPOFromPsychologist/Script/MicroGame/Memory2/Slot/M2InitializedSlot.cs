using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class M2InitializedSlot : MonoBehaviour
    {
        [Inject] private M2Resources m2Resources;

        private List<M2ImageSlot> m2ImageSlots;
        public List<M2ImageSlot> M2ImageSlots { get { return m2ImageSlots; }}


        private M2ImageSlot GetFirstChildren(Transform slot)
        {
            if (slot.childCount > 1 || slot.childCount == 0)
            {
                Debug.LogWarning("Найдено больше одного ребёнка или ненайднно не одного");
                return null;
            }

            if (slot.GetChild(slot.childCount - 1).TryGetComponent<M2ImageSlot>(out var m2Slot))
                return m2Slot;
            else
            {
                Debug.LogWarning("Ненайдено не одного компонента M2ImageSlot");
                return null;
            }
        }


        public void Initialized(List<Transform> slots, List<Sprite> sprites)
        {
            m2ImageSlots = new();

            foreach (Transform child in slots)
            {
                m2ImageSlots.Add(GetFirstChildren(child));
            }

            if (m2ImageSlots.Count != sprites.Count)
            {
                Debug.LogError("Массивы m2ImageSlot и sprites не совпадают по размерности");
                return;
            }


            for (int i = 0; i < m2ImageSlots.Count; i++)
            {
                m2ImageSlots[i].ImageSlot.sprite = sprites[i];
                m2ImageSlots[i].Initialized();

                if (m2ImageSlots[i].TryGetComponent<M2DragAndDrop>(out var dragSystem))
                {
                    m2Resources.listPropDragAndDrop.Add(dragSystem);
                }            
            }
        }
    }
}
