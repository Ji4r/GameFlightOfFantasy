using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

namespace DiplomGames
{
    public class AnimsTooltip : IDisposable
    {
        private float durationAnims;
        private float alpha;
        private float alphaText;

        private Transform objTrans;
        private Image image;
        private Tween animPanel;
        private Tween animText;

        public AnimsTooltip(float durationAnims, Transform objTrans, Image image)
        {
           this.durationAnims = durationAnims;        
           this.objTrans = objTrans;
           this.image = image;
           alpha = image.color.a;
        }
        public AnimsTooltip(float durationAnims, Transform objTrans, Image image, TextMeshProUGUI text)
        {
            this.durationAnims = durationAnims;
            this.objTrans = objTrans;
            this.image = image;
            alpha = image.color.a;
            alphaText = text.color.a;
        }

        public void Show()
        {
            KillAnims();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

            objTrans.gameObject.SetActive(true);

            animPanel = image.DOFade(alpha, durationAnims);
        }
        public void Hide()
        {
            KillAnims();

            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            animPanel = image.DOFade(0, durationAnims).OnComplete(() => 
            {
                objTrans.gameObject.SetActive(false);
            });
        }

        public void Show(TextMeshProUGUI text)
        {
            KillAnims();
            Debug.Log(text.color.a);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

            objTrans.gameObject.SetActive(true);
            text.gameObject.SetActive(true);

            animPanel = image.DOFade(alpha, durationAnims);
            animText = text.DOFade(alphaText, durationAnims);
        }

        public void Hide(TextMeshProUGUI text)
        {
            KillAnims();

            text.color = new Color(text.color.r, text.color.g, text.color.b, alphaText);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            animText = text.DOFade(0, durationAnims).OnComplete(() =>
            {
                text.gameObject.SetActive(false);
            });

            animPanel = image.DOFade(0, durationAnims).OnComplete(() =>
            {
                objTrans.gameObject.SetActive(false);
            });
        }

        public void KillAnims()
        {
            if (animPanel != null)
            {
                animPanel.Kill();
            }
            if (animPanel != null)
            {
                animText.Kill();
            }
        }

        public void Dispose()
        {
            KillAnims();
        }
    }
}
