using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task CardMoveToSlot(Transform objectCard, Transform slots)
        {
            KillAllAnims();

            objectCard.SetParent(slots);
            objectCard.position = objectCard.position;

            Tween tween = objectCard.DOLocalMove(Vector3.zero, durationAnims);
            tweens.Add(tween);

            await tween.AsyncWaitForCompletion();
        }

        public async Task CardsMoveToSlot(Transform[] cards, Transform[] slots)
        {
            if (!ArrayIsValidate(cards, slots)) return;

            KillAllAnims();

            var tasks = new List<Task>();

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                var targetSlot = slots[i];

                Vector3 worldPos = card.position;
                card.SetParent(targetSlot);
                card.position = worldPos;

                var task = card.DOLocalMove(Vector3.zero, durationAnims)
                    .AsyncWaitForCompletion();
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public async Task CardsMoveOnStartPosition(Transform[] cards, Transform[] slots)
        {
            if (!ArrayIsValidate(cards, slots)) return;

            KillAllAnims();

            var tasks = new List<Task>();

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];

                Vector3 currentWorldPos = card.position;
                card.SetParent(pointStartCard);
                card.position = currentWorldPos;

                var task = card.DOLocalMove(Vector3.zero, durationAnims)
                    .AsyncWaitForCompletion();
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public async Task CardMoveOnStartPosition(Transform card, Transform slot)
        {
            if (card == null || slot == null) 
                throw new System.Exception("card или slot равен null");

            KillAllAnims();
            card.SetParent(slot);

            var task = card.DOLocalMove(Vector3.zero, durationAnims)
                .AsyncWaitForCompletion();

            await task;
        }

        public async Task Awaittime(int time)
        {
            await Task.Delay(time);
        }

        private bool ArrayIsValidate(Transform[] cards, Transform[] slots)
        {
            if (cards == null || slots == null)
            {
                Debug.LogWarning(" акой то из массивов не создан! Ќе возможно передвинуть карты в слоты");
                return false;
            }

            if (cards.Length != slots.Length)
            {
                Debug.LogWarning($"–азмерность массивов cardList и slots разна€ не возможно передвинуть карты в слоты!");
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
