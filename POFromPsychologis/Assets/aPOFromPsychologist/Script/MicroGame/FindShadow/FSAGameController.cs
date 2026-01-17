using UnityEngine;
using System;

namespace DiplomGames
{
    public class FSAGameController : GameController
    {
        [SerializeField] private FSASlotManager slotManager;
        [SerializeField] private FSAChecketSlot checkerSlot;
        [SerializeField] private FSAUiView uiView;
        [Inject] private PlayPhrasesVetricksOnCall playPhrasesVetricksOnCall;

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
            playPhrasesVetricksOnCall.PlayPhraseAndHideVetrick();
            await slotManager.NextGame();
            await slotManager.SetScaleToZero();
            currentGame = slotManager.GeneratedNewLevel();
            uiView.UpdateSpriteProp(currentGame.Item1);
            await slotManager.SetScaleToBase();
            checkerSlot.UpdateRightQuestion(currentGame.Item2);
        }
    }
}
