using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class M2GameManager : GameController
    {
        public static M2GameManager instance;
        [SerializeField] private ScriptableMover presetMoverAnim;
        [SerializeField] private SliderLevelComplexity sliderLevelComplexity;
        [SerializeField] private Button startMove;
        [SerializeField] private Button btnNextRound;
        [SerializeField] private Button btnNewDiffecalty;
        [SerializeField] private Button btnExitMenu;
        [SerializeField] private GameObject panelStart;
        [SerializeField] private M2FactoryCreateSlots fabricSlots;
        [SerializeField] private M2InitializedSlot initializedSlot;
        
        [Inject] private M2Resources resources;
        [Inject] private EntryPoint entryPoint;
        [Inject] private M2UiView uiView;
        [Inject] private PlayPhrasesVetricksOnCall playPhrase;

        private bool isDontFirstGame;
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
            sliderLevelComplexity.AcceptComplexityChanged += StartGames;
            startMove.onClick.AddListener(StartMoveAnimToSlot);

            btnNextRound.onClick.AddListener(NextRound);
            btnNewDiffecalty.onClick.AddListener(NewDiffecalty);
            btnExitMenu.onClick.AddListener(() => { entryPoint.LoadScene(1); });
        }

        private void OnDisable()
        {
            sliderLevelComplexity.AcceptComplexityChanged -= StartGames;
            startMove.onClick.RemoveListener(StartMoveAnimToSlot);

            btnNextRound.onClick.RemoveListener(NextRound);
            btnNewDiffecalty.onClick.RemoveListener(NewDiffecalty);
            btnExitMenu.onClick.RemoveListener(() => { entryPoint.LoadScene(1); });
        }

        private void StartGames(LevelComplexity size)
        {
            sizeField = size.CurrentLevelComplexity;
            fabricSlots.Initialized(size.CurrentLevelComplexity);
            ganerator = new M2Generator();
            var listSprite = ganerator.Generate(resources.listSprite, size.CurrentLevelComplexity);
            initializedSlot.Initialized(fabricSlots.GameFieldTransform, listSprite);
            

            panelStart.SetActive(false);

            if (isDontFirstGame == false)
            {
                playPhrase.PlayWelcomePhrase();
                isDontFirstGame = true;
            }

            foreach (var slot in resources.listPropDragAndDrop)
            {
                slot.enabled = false;
            }
        }

        private void NewDiffecalty()
        {
            uiView.SetEnabledPanelEndGame(false);
            uiView.SetEnabledPanelSelectDiffecalty(true);
        }

        protected override void NextRound()
        {
            uiView.SetEnabledPanelEndGame(false);
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
            startMove.interactable = false;
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

            foreach (var slot in initializedSlot.M2ImageSlots) // Включаем обратно Blocks Raycast у Canvas Group
            {
                if (slot.TryGetComponent<M2DragAndDrop>(out var dragSystem))
                {
                    dragSystem.SetRaycast(true);
                }
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
            uiView.SetEnabledPanelEndGame(true);
            startMove.interactable = true;
        }
    }
}
