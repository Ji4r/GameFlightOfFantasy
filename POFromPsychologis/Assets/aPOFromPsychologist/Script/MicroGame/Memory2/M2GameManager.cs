using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class M2GameManager : GameController
    {
        public static M2GameManager instance;

        [SerializeField] private ScriptableMover presetMoverAnim;
        [SerializeField] private Button startGame5;
        [SerializeField] private Button startGame10;
        [SerializeField] private Button startMove;
        [SerializeField] private GameObject panelStart;
        [SerializeField] private M2FactoryCreateSlots fabricSlots;
        [SerializeField] private M2InitializedSlot initializedSlot;
        [Inject] private M2Resources resources;

        private M2Generator ganerator;
        private int sizeField;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        private void OnEnable()
        {
            startGame5.onClick.AddListener(() => { StartGames(5); });
            startGame10.onClick.AddListener(() => { StartGames(10); });
            startMove.onClick.AddListener(StartMoveAnimToSlot);
        }

        private void OnDisable()
        {
            startGame5.onClick.RemoveListener(() => { StartGames(5); });
            startGame10.onClick.RemoveListener(() => { StartGames(10); });
            startMove.onClick.RemoveListener(StartMoveAnimToSlot);
        }

        private void StartGames(int size)
        {
            sizeField = size;
            fabricSlots.Initialized(size);
            ganerator = new M2Generator();
            var listSprite = ganerator.Generate(resources.listSprite, size);
            initializedSlot.Initialized(fabricSlots.GameFieldTransform, listSprite);
            

            panelStart.SetActive(false);

            foreach (var slot in resources.listPropDragAndDrop)
            {
                slot.enabled = false;
            }
        }

        protected override void NextRound()
        {
            fabricSlots.Initialized(sizeField);
            var listSprite = ganerator.Generate(resources.listSprite, sizeField);
            initializedSlot.Initialized(fabricSlots.GameFieldTransform, listSprite);

            foreach (var item in fabricSlots.GameM2SlotsChecker)
            {
                item.ResetState();
            }

            foreach (var slot in initializedSlot.M2ImageSlots) // Включаем обратно Blocks Raycast у Canvas Group
            {
                if (slot.TryGetComponent<M2DragAndDrop>(out var dragSystem))
                {
                    dragSystem.SetRaycast(true);
                }
            }

            foreach (var slot in resources.listPropDragAndDrop)
            {
                slot.enabled = false;
            }
        }

        private void StartMoveAnimToSlot()
        {
            var anim = new MoveToSlotAnims(presetMoverAnim);

            if (fabricSlots == null)
            {
                Debug.LogError("Не найден M2FactoryCreateSlots");
                return;
            }

            var listPlayerSlot = fabricSlots.PlayerFieldTransform;


            var shuffledList = new List<Transform>();
            shuffledList.AddRange(listPlayerSlot);

            for (int i = shuffledList.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                var temp = shuffledList[i];
                shuffledList[i] = shuffledList[j];
                shuffledList[j] = temp;
            }

            for (int i = 0; i < shuffledList.Count; i++)
            {
                anim.MoveToSlot(initializedSlot.M2ImageSlots[i].transform, shuffledList[i]);
                initializedSlot.M2ImageSlots[i].transform.SetParent(shuffledList[i]);
            }

            foreach (var slot in resources.listPropDragAndDrop)
            {
                slot.enabled = true;
            }
        }

        public void CheckIsRepliedRight()
        {
            if (initializedSlot == null)
            {
                Debug.LogError("Не найден M2FactoryCreateSlots");
                return;
            }

            foreach (var item in fabricSlots.GameM2SlotsChecker)
            {
                if (item.IsReplied == false)
                {
                    return;
                }
            }

            Debug.Log("ИГРА ПРОЙДЕНА!");
            NextRound();
        }
    }
}
