using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class PanelAnims : MonoBehaviour
    {
        [Header("Задний экран")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private float setOpacity = 0.5f;
        [SerializeField] private float durationAnimsOnFade = 0.2f;
        [SerializeField] private Ease easeScheduleOpacity = Ease.OutCubic;

        [Header("Панель")]
        [SerializeField] private Transform panel;
        [SerializeField] private float durationAnimsOnPanel = 0.2f;
        [SerializeField] private Ease easeSchedulePanel = Ease.OutCubic;

        private Vector3 originalScale;
        private Transform btnTransform;
        private Tween animsOpacity;
        private Tween animsPanel;
        private GameObject gameObjectPanel;
        private GameObject gameObjectBackground;


        private void Start()
        {
            originalScale = panel.localScale;
            if (backgroundImage != null)
            {
                gameObjectBackground = backgroundImage.gameObject;
                backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);
            }

            gameObjectPanel = panel.gameObject;
        }

        public void Hide()
        {
            KillAnims();

            if (backgroundImage != null)
            {
                animsOpacity = backgroundImage.DOFade(0f, durationAnimsOnFade).SetEase(easeScheduleOpacity).OnComplete(() =>
                {
                    gameObjectBackground.SetActive(false);
                });
            }
            animsPanel = panel.DOScale(Vector3.zero, durationAnimsOnPanel).SetEase(easeSchedulePanel).OnComplete(() => {
                gameObjectPanel.SetActive(false);
            });
        }

        public void Show()
        {
            KillAnims();

            panel.localScale = Vector3.zero;
            gameObjectPanel.SetActive(true);
            gameObjectBackground.SetActive(true);
            if (backgroundImage != null)
                animsOpacity = backgroundImage.DOFade(setOpacity, durationAnimsOnFade).SetEase(easeScheduleOpacity);

            animsPanel = panel.DOScale(originalScale, durationAnimsOnPanel).SetEase(easeSchedulePanel);
        }

        private void KillAnims()
        {
            if (animsOpacity != null)
            {
                animsOpacity.Kill();
            }

            if (animsPanel != null)
            {
                animsPanel.Kill();
            }
        }

        private void OnDisable()
        {
            KillAnims();
        }
    }
}
