using UnityEngine;
using System;
using System.Threading.Tasks;

namespace DiplomGames
{
    public class FSAGameController : GameController
    {
        [SerializeField] private FSASlotManager slotManager;
        [SerializeField] private FSAChecketSlot checkerSlot;
        [SerializeField] private FSAUiView uiView;

        public Action StartNextGame;

        private (Sprite, Transform) currentGame;

        private void OnEnable()
        {
            StartNextGame += NextRound;
        }


        private void Start()
        {
            StartGame();
        }

        private void OnDisable()
        {
            StartNextGame -= NextRound;
        }

        protected override void StartGame()
        {
            currentGame = slotManager.StartGame();
            uiView.UpdateSpriteProp(currentGame.Item1);
            checkerSlot.UpdateRightQuestion(currentGame.Item2);
        }

        protected override void EndGame()
        {
            
        }

        protected override async void NextRound()
        {
            currentGame = await slotManager.NextGame();
            uiView.UpdateSpriteProp(currentGame.Item1);
            checkerSlot.UpdateRightQuestion(currentGame.Item2);
        }
    }
}
