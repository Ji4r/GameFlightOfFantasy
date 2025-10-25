using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class FSAnimatedCards
    {
        private float durationAnims;
        private Transform pointStartCard;

        private List<Tween> tweens;


        public FSAnimatedCards(Transform pointStartCard, float durationAnims)
        {
            this.pointStartCard = pointStartCard;
            this.durationAnims = durationAnims;
            tweens = new List<Tween>();
        }

        public void CardMoveToSlot(Transform[] cards, Transform[] slots)
        {
            if (!ArrayIsValidate(cards, slots)) return;

            KillAllAnims();

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                int currentIndex = i;

                var targetSlot = slots[currentIndex];
                Vector3 worldPos = card.position;
                card.SetParent(targetSlot);
                card.position = worldPos;
                Tween tween = card.DOLocalMove(Vector3.zero, durationAnims);
                tweens.Add(tween);
            }
        }

        public void CardMoveOnStartPosition(Transform[] cards, Transform[] slots, Action callback = null)
        {
            if (!ArrayIsValidate(cards, slots)) return;

            KillAllAnims();

            bool[] allAinmsReady = new bool[cards.Length];
            int completedAnimations = 0;

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                int currentIndex = i;

                Vector3 currentWorldPos = card.position;
                card.SetParent(pointStartCard);
                card.position = currentWorldPos;

                Tween tween = card.DOLocalMove(Vector3.zero, durationAnims).OnComplete(() => {
                    allAinmsReady[currentIndex] = true;
                    completedAnimations++;

                    if (completedAnimations == cards.Length)
                        callback?.Invoke();
                });
                tweens.Add(tween);
            }
        }


        private bool ArrayIsValidate(Transform[] cards, Transform[] slots)
        {
            if (cards == null || slots == null)
            {
                Debug.LogWarning("Какой то из массивов не создан! НЕ возможно передвинуть карты в слоты");
                return false;
            }

            if (cards.Length != slots.Length)
            {
                Debug.LogWarning($"Размерность массивов cardList и slots разная не возможно передвинуть карты в слоты!");
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            KillAllAnims();
        }

        private void KillAllAnims()
        {
            foreach (var item in tweens)
            {
                item.Kill();
            }

            tweens.Clear();
        }
    }
}
