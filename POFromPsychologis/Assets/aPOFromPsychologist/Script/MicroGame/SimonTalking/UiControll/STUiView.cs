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

        [Inject] private STBuilderGame builderGame;
        [Inject] private STColorValidator colorValidator;
        [Inject] private STGameController gameController;
        [Inject] private STGameSettingsManager gameSettingsManager;
        [Inject] private STHistoryColor historyColor;
        [Inject] private STSimonWheel simonWheel;

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
        }

        public void InitRangeDifficulties(STDifficultiesPreset difficultiesPreset)
        {
            gameSettingsManager.difficultiesPreset = difficultiesPreset;
        }

        public void InitWheel(STGamePreset wheelPreset)
        {
            gameSettingsManager.gamePreset = wheelPreset;
            InitializedGame();
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
