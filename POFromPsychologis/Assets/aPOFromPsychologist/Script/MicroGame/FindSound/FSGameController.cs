using System;
using UnityEngine;

namespace DiplomGames
{
    public class FSGameController : GameController
    {
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private FSCheckerSlot checkerSlot; 
        [SerializeField] private UiViewFS uiViewFS;

        public Action StartNextGame;

        private FSSoundList currentGame;

        private void OnEnable()
        {
            StartNextGame += NextRound;
        }

        private void OnDisable()
        {
            StartNextGame -= NextRound;
        }

        private void Start()
        {
            currentGame = slotManager.StartGame();
            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);
        }

        protected override void NextRound()
        {
            currentGame = slotManager.NextGame();
            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);
        }
    }
}
