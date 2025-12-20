using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class M2FactoryCreateSlots : MonoBehaviour
    {
        [SerializeField] private Transform gameField;
        [SerializeField] private Transform playerField;
        [SerializeField] private bool autoExtance;
        [SerializeField] private M2SlotChecker prefabCard;
        [SerializeField] private M2Slot prefabCarsSlotClear;

        private List<Transform> playerFieldTransform;
        private List<Transform> gameFieldTransform;
        private List<M2SlotChecker> gameM2SlotsChecker;

        public List<M2SlotChecker> GameM2SlotsChecker { get { return gameM2SlotsChecker; } }
        public List<Transform> PlayerFieldTransform
        { get { return playerFieldTransform; } }
        public List<Transform> GameFieldTransform
        { get { return gameFieldTransform; } }

        public void Initialized(int createSlot)
        {
            SetActiveChildren(gameField, GetChildrenSlots(gameField), 
            createSlot, prefabCard.gameObject);
            SetActiveChildren(playerField, GetChildrenSlots(playerField), 
            createSlot, prefabCarsSlotClear.gameObject);

            FillField();
        }

        private List<Transform> GetChildrenSlots(Transform parent)
        {
            List<Transform> childs = new();

            for (int i = 0; i < parent.childCount; i++)
            {
                childs.Add(parent.GetChild(i));
            }

            return childs;
        }

        private void SetActiveChildren(Transform parent, List<Transform>childs, int countChilren, GameObject prefab)
        {
            foreach (Transform child in childs)
            {
                child.gameObject.SetActive(false);
            }

            if (childs.Count < countChilren)
            {
                if (autoExtance)
                {
                    int createElement = countChilren - childs.Count;
                    for (int i = 0; i < createElement; i++)
                    {
                        childs.Add(CreateElement(parent, prefab, false).transform);
                    }
                }
                else
                {
                    Debug.LogError("выключено расширение, и вы пытаетесь создать слишком много элементов");
                    return;
                }
            }

            for (int i = 0; i < countChilren && i < childs.Count; i++)
            {
                childs[i].gameObject.SetActive(true);
            }
        }

        private GameObject CreateElement(Transform parent, GameObject prefab, bool isActive = false)
        {
            var obj = Instantiate(prefab, parent);
            obj.gameObject.SetActive(isActive);
            return obj;
        }

        private void FillField()
        {
            if (gameM2SlotsChecker == null)
                gameM2SlotsChecker = new();


            playerFieldTransform = GetChildrenSlots(playerField);
            gameFieldTransform = GetChildrenSlots(gameField);

            foreach (var item in gameFieldTransform)
            {
                if (item.TryGetComponent<M2SlotChecker>(out var childSlot))
                {
                    gameM2SlotsChecker.Add(childSlot);
                }
            }
        }
    }
}
