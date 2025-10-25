using System;
using UnityEngine;

namespace DiplomGames
{
    public class FSGameController : MonoBehaviour
    {
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private CheckerSlot checkerSlot; 
        [SerializeField] private UiViewFS uiViewFS;

        public Action StartNextGame;

        private FSSoundList currentGame;

        private void OnEnable()
        {
            StartNextGame += NextGame;
        }

        private void OnDisable()
        {
            StartNextGame -= NextGame;
        }

        private void Start()
        {
            currentGame = slotManager.StartGame();
            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);
        }

        private void NextGame()
        {
            currentGame = slotManager.NextGame();
            uiViewFS.UpdateSpriteProp(currentGame.Sprite);
            checkerSlot.UpdateRightSound(currentGame.TheRightSound);
        }
    }
}
