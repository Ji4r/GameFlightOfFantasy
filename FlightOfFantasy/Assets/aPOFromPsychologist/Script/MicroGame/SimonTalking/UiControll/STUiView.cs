using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class STUiView : MonoBehaviour
    {
        [Header("Ссылки на элементы")]
        [SerializeField] private GameObject windowRestratGame;
        [SerializeField] private TextMeshProUGUI textEventGame;
        [SerializeField] private TextPreset textIsWin = new TextPreset("Правильно!", Color.green);
        [SerializeField] private TextPreset textIsLose = new TextPreset("Ошибка!", Color.red);

        [Header("Ссылки на кнопки")]
        [SerializeField] private Button buttonReplay;
        [SerializeField] private Button buttonNextRoundOnWindow;
        [SerializeField] private Button buttonStartGame;
        [SerializeField] private Button buttonPlaySequence;
        [SerializeField] private Button prevStep;

        [Header("Ссылки на панели")]
        [SerializeField] private GameObject panelStart;
        [SerializeField] private GameObject panelEnd;
        [SerializeField] private GameObject PhirstStepSelectionDiffecalty;
        [SerializeField] private GameObject NextStepSelectionDiffecalty;

        [Header("Кнопки")]
        [SerializeField] private Button btnNextGame;
        [SerializeField] private Button btnChangeDiffecalty;
        [SerializeField] private Button btnGoToMenu;


        [Inject] private EntryPoint entryPoint;
        [Inject] private STBuilderGame builderGame;
        [Inject] private STColorValidator colorValidator;
        [Inject] private STGameController gameController;
        [Inject] private STGameSettingsManager gameSettingsManager;
        [Inject] private STHistoryColor historyColor;
        [Inject] private STSimonWheel simonWheel;
        [Inject] private PlayPhrasesVetricksOnCall playPhrase;

        private bool isDontFirstGame;
        private bool isInitialized;

        private void OnEnable()
        {
            if (!isInitialized) return;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            buttonReplay.onClick.AddListener(OnRestartClick);
            buttonNextRoundOnWindow.onClick.AddListener(OnNextClick);
            colorValidator.AnErrorWasMade += ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect += EverythingIsCorrect;
            buttonStartGame.onClick.AddListener(StartGame);
            buttonPlaySequence.onClick.AddListener(NextRound);
            prevStep.onClick.AddListener(PrevStep);

            btnNextGame.onClick.AddListener(NextRound);
            btnChangeDiffecalty.onClick.AddListener(NewDiffecalty);
            btnGoToMenu.onClick.AddListener(() => { entryPoint.LoadScene(1); });
        }

        private void OnDisable()
        {
            if (!isInitialized) return;

            buttonReplay.onClick.RemoveListener(OnRestartClick);
            buttonNextRoundOnWindow.onClick.RemoveListener(OnNextClick);
            colorValidator.AnErrorWasMade -= ShowWindowRestartGame;
            colorValidator.EverythingIsCorrect -= EverythingIsCorrect;
            buttonStartGame.onClick.RemoveListener(StartGame);
            buttonPlaySequence.onClick.RemoveListener(NextRound);
            prevStep.onClick.RemoveListener(PrevStep);

            btnNextGame.onClick.RemoveListener(NextRound);
            btnChangeDiffecalty.onClick.RemoveListener(NewDiffecalty);
            btnGoToMenu.onClick.AddListener(() => { entryPoint.LoadScene(1); });
        }

        public void InitRangeDifficulties(STDifficultiesPreset difficultiesPreset)
        {
            gameSettingsManager.difficultiesPreset = difficultiesPreset;

            PhirstStepSelectionDiffecalty.SetActive(false);
            NextStepSelectionDiffecalty.SetActive(true);
        }

        public void InitWheel(STGamePreset wheelPreset)
        {
            gameSettingsManager.gamePreset = wheelPreset;
            InitializedGame();


            if (isDontFirstGame == false)
            {
                playPhrase.PlayWelcomePhrase();
                isDontFirstGame = true;
            }
        }

        private void Init()
        {
            if (colorValidator == null || gameController == null)
            {
                Debug.LogError("Dependencies not injected!");
                return;
            }

            SubscribeToEvents();
            isInitialized = true;
        }

        private void OnRestartClick()
        {
            gameController.RestartGameEvent?.Invoke();
            colorValidator.CleatInputList();
            windowRestratGame.SetActive(false);
            gameController.RestartGameEvent?.Invoke();
        }

        private void OnNextClick()
        {
            if (!buttonPlaySequence.gameObject.activeInHierarchy)
                buttonPlaySequence.gameObject.SetActive(true);
            gameController.SetActivePianino(false);
            buttonPlaySequence.interactable = true;
            windowRestratGame.SetActive(false);
        }

        private async void ShowWindowRestartGame()
        {
            textEventGame.color = textIsLose.Color;
            textEventGame.text = textIsLose.Text;
            SoundPlayer.instance.PlaySound(ListSound.answerNotSuccesful);
            Task shakeAnims = simonWheel.StartShakeWheel();
            Task clearHistoryAnims = historyColor.ClearHistory();
            await shakeAnims;
            await clearHistoryAnims;
            windowRestratGame.SetActive(true);
            textEventGame.text = string.Empty;
        }

        private async void EverythingIsCorrect()
        {
            if (!buttonPlaySequence.gameObject.activeInHierarchy)
                buttonPlaySequence.gameObject.SetActive(true);

            textEventGame.color = textIsWin.Color;
            textEventGame.text = textIsWin.Text;
            windowRestratGame.SetActive(false);
            gameController.SetActivePianino(false);
            SoundPlayer.instance.PlaySound(ListSound.AllAnswerCorrectInMemory);
            await historyColor.ClearHistory();
            buttonPlaySequence.interactable = true;
            textEventGame.text = string.Empty;
            panelEnd.SetActive(true);
        }

        private void InitializedGame()
        {
            builderGame.CreateObject(gameSettingsManager);
        }

        private void StartGame()
        {
            buttonPlaySequence.interactable = false;
            buttonStartGame.gameObject.SetActive(false);
            gameController.StartGameEvent?.Invoke(gameSettingsManager);
        }

        private void NextRound()
        {
            buttonPlaySequence.interactable = false;
            gameController.NextGameEvent?.Invoke();
            panelEnd.SetActive(false);
        }

        private void PrevStep()
        {
            PhirstStepSelectionDiffecalty.SetActive(true);
            NextStepSelectionDiffecalty.SetActive(false);
        }

        public void NewDiffecalty()
        {
            simonWheel.ClearWheelData();
            builderGame.ClearPianino();

            panelStart.SetActive(true);
            panelEnd.SetActive(false);

            PrevStep();
        }
    }

    [System.Serializable]
    public struct TextPreset
    {
        public string Text;
        public Color Color;


        public TextPreset(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
}
