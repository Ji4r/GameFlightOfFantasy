using System;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class MGameController : GameController
    {
        [SerializeField] private Button button4;
        [SerializeField] private Button button6;
        [SerializeField] private Button button8;
        [SerializeField] private Button btnRestart;
        [SerializeField] private GameObject panelWindow;
        [SerializeField] private GameObject panelEndGame;
        [SerializeField] private GameObject panelPlayingFields;
        [SerializeField] private MGeneratedLevel generatedLevel;

        public Action EndGameAction;

        private int fieldGame;

        void Start()
        {
            panelWindow.SetActive(true);            
        }

        private void OnEnable()
        {
            btnRestart.onClick.AddListener(RestartGame);
            button4.onClick.AddListener(() => { StartGenerate(8); });
            button6.onClick.AddListener(() => { StartGenerate(12); });
            button8.onClick.AddListener(() => { StartGenerate(16); });
            EndGameAction += EndGame;
        }

        private void OnDisable()
        {
            btnRestart.onClick.RemoveListener(RestartGame);
            button4.onClick.RemoveListener(() => { StartGenerate(8); });
            button6.onClick.RemoveListener(() => { StartGenerate(12); });
            button8.onClick.RemoveListener(() => { StartGenerate(16); });
            EndGameAction -= EndGame;
        }

        private void StartGenerate(int size)
        {
            panelWindow.SetActive(false);
            generatedLevel.GenerateLevel(size);
        }

        protected override void EndGame()
        {
            panelPlayingFields.SetActive(false);
            panelEndGame.SetActive(true);
        }

        protected override void RestartGame()
        {
            SwitchScene.RestartScene();
        }
    }
}
