using UnityEngine;
using System;

namespace DiplomGames
{
    public class FSAGameController : GameController
    {
        [SerializeField] private FSASlotManager slotManager;
        [SerializeField] private CheckerSlot checkerSlot;
        [SerializeField] private FSAUiView uiView;

        public Action StartNextGame;

        private Sprite currentGame;

        private void OnEnable()
        {
            StartNextGame += NextRound;
        }

        private void OnDisable()
        {
            StartNextGame -= NextRound;
        }

        protected override void StartGame()
        {
            currentGame = slotManager.StartGame();
            uiView.UpdateSpriteProp(currentGame);
           // checkerSlot.UpdateRightQuestion(currentGame);
        }

        protected override void EndGame()
        {
            
        }

        protected override void NextRound()
        {
            //currentGame = slotManager.NextGame();
            //uiView.UpdateSpriteProp(currentGame.Sprite);
            //checkerSlot.UpdateRightSound(currentGame.TheRightSound);
        }
    }
}
