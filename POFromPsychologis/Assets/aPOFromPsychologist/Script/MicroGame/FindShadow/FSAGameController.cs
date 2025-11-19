using UnityEngine;
using System;

namespace DiplomGames
{
    public class FSAGameController : GameController
    {
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private FSCheckerSlot checkerSlot;
        [SerializeField] private UiViewFS uiViewFS;

        public Action StartNextGame;

        private FSSoundList currentGame;

        private void OnEnable()
        {
            StartNextGame += NextRaund;
        }

        private void OnDisable()
        {
            StartNextGame -= NextRaund;
        }

        protected override void StartGame()
        {
            currentGame = slotManager.StartGame();
            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);
        }

        protected override void EndGame()
        {
            
        }

        protected override void NextRaund()
        {
            currentGame = slotManager.NextGame();
            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);
        }
    }
}
