using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DiplomGames
{
    public class LuckyJetGame : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float acceleration = 0.1f;
        [SerializeField] private float crashProbability = 0.5f;
        [SerializeField] private float maxMultiplier = 100f;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI multiplierText;
        [SerializeField] private TextMeshProUGUI balanceText;
        [SerializeField] private TextMeshProUGUI betAmountText;
        [SerializeField] private TextMeshProUGUI winAmountText;
        [SerializeField] private TextMeshProUGUI gameStatusText;
        [SerializeField] private Button betButton;
        [SerializeField] private Button cashoutButton;
        [SerializeField] private Button increaseBetButton;
        [SerializeField] private Button decreaseBetButton;
        [SerializeField] private Slider multiplierPredictorSlider;

        [Header("Jet Settings")]
        [SerializeField] private Transform jetTransform;
        [SerializeField] private ParticleSystem jetTrail;
        [SerializeField] private ParticleSystem explosionEffect;
        [SerializeField] private AudioClip cashoutSound;
        [SerializeField] private AudioClip explosionSound;
        [SerializeField] private AudioClip jetSound;

        private float currentMultiplier = 1.0f;
        private float currentSpeed;
        private float playerBalance = 1000f;
        private float currentBet = 100f;
        private float potentialWin = 0f;
        private bool isGameRunning = false;
        private bool isCrashed = false;
        private Vector3 jetStartPosition;
        private AudioSource audioSource;

        private Coroutine gameCoroutine;
        public bool IsGameRunning => isGameRunning;

        public float CurrentMultiplier => currentMultiplier;

        private float targetMultiplier = 5f;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            jetStartPosition = jetTransform.position;

            SetupUI();
            UpdateUI();
        }

        public bool CanPlaceBet()
        {
            return !isGameRunning && currentBet <= playerBalance;
        }

        public void SetTargetMultiplier(float target)
        {
            targetMultiplier = Mathf.Clamp(target, 1.1f, maxMultiplier);
        }

        // В методе Update() или GameLoop() добавьте автозабор при достижении цели:
        private void CheckAutoCashout()
        {
            if (isGameRunning && !isCrashed && currentMultiplier >= targetMultiplier)
            {
                CashOut();
            }
        }

        void SetupUI()
        {
            betButton.onClick.AddListener(StartGame);
            cashoutButton.onClick.AddListener(CashOut);
            increaseBetButton.onClick.AddListener(() => ChangeBet(100f));
            decreaseBetButton.onClick.AddListener(() => ChangeBet(-100f));

            cashoutButton.interactable = false;
            multiplierPredictorSlider.maxValue = maxMultiplier;
        }

        void UpdateUI()
        {
            balanceText.text = $"Баланс: ${playerBalance:F2}";
            betAmountText.text = $"Ставка: ${currentBet:F2}";
            multiplierText.text = $"Множитель: {currentMultiplier:F2}x";
            winAmountText.text = $"Выигрыш: ${potentialWin:F2}";
        }

        void ChangeBet(float amount)
        {
            if (isGameRunning) return;

            currentBet = Mathf.Clamp(currentBet + amount, 10f, playerBalance);
            UpdateUI();
        }

        public void StartGame()
        {
            if (isGameRunning || currentBet > playerBalance) return;

            playerBalance -= currentBet;
            isGameRunning = true;
            isCrashed = false;
            currentMultiplier = 1.0f;
            currentSpeed = minSpeed;
            potentialWin = currentBet * currentMultiplier;

            // Сброс позиции реактивного самолета
            jetTransform.position = jetStartPosition;
            jetTransform.gameObject.SetActive(true);
            jetTrail.Play();

            // Активация кнопок
            betButton.interactable = false;
            cashoutButton.interactable = true;
            increaseBetButton.interactable = false;
            decreaseBetButton.interactable = false;

            // Запуск звука
            audioSource.clip = jetSound;
            audioSource.loop = true;
            audioSource.Play();

            gameStatusText.text = "Игра идет...";
            gameStatusText.color = Color.green;

            if (gameCoroutine != null)
                StopCoroutine(gameCoroutine);

            gameCoroutine = StartCoroutine(GameLoop());
        }

        IEnumerator GameLoop()
        {
            while (isGameRunning && !isCrashed)
            {
                yield return null;

                // Увеличение скорости и множителя
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
                CheckAutoCashout();
                currentMultiplier += currentSpeed * Time.deltaTime * 0.5f;
                potentialWin = currentBet * currentMultiplier;

                // Движение самолета
                jetTransform.Translate(Vector3.right * currentSpeed * Time.deltaTime);

                // Проверка на краш
                if (Random.value < crashProbability * Time.deltaTime)
                {
                    Crash();
                }

                // Обновление предсказателя
                float predictedCrash = Mathf.Clamp(currentMultiplier + Random.Range(1f, 10f), 1f, maxMultiplier);
                multiplierPredictorSlider.value = predictedCrash;

                UpdateUI();

                // Автоматический краш на максимальном множителе
                if (currentMultiplier >= maxMultiplier)
                {
                    Crash();
                }
            }
        }

        void Crash()
        {
            if (isCrashed) return;

            isCrashed = true;
            isGameRunning = false;

            // Эффекты взрыва
            explosionEffect.transform.position = jetTransform.position;
            explosionEffect.Play();
            jetTransform.gameObject.SetActive(false);
            jetTrail.Stop();

            // Звук взрыва
            audioSource.Stop();
            audioSource.PlayOneShot(explosionSound);

            gameStatusText.text = "КРАШ!";
            gameStatusText.color = Color.red;

            ResetButtons();
        }

        void CashOut()
        {
            if (!isGameRunning || isCrashed) return;

            isGameRunning = false;

            // Выигрыш
            float winAmount = currentBet * currentMultiplier;
            playerBalance += winAmount;

            // Эффекты
            jetTrail.Stop();
            jetTransform.gameObject.SetActive(false);
            audioSource.Stop();
            audioSource.PlayOneShot(cashoutSound);

            gameStatusText.text = $"Вы забрали {currentMultiplier:F2}x!";
            gameStatusText.color = Color.yellow;

            ResetButtons();
            UpdateUI();
        }

        void ResetButtons()
        {
            betButton.interactable = true;
            cashoutButton.interactable = false;
            increaseBetButton.interactable = true;
            decreaseBetButton.interactable = true;

            if (gameCoroutine != null)
            {
                StopCoroutine(gameCoroutine);
                gameCoroutine = null;
            }
        }

        void OnDestroy()
        {
            if (gameCoroutine != null)
                StopCoroutine(gameCoroutine);
        }
    }
}
