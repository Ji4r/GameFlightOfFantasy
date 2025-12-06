using System;
using TMPro;
using UnityEngine;

namespace DiplomGames
{
    public class SetBet : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        public int bet;
        [SerializeField] private Balance balance;

        public void Bet()
        {
            if (bet > balance.balance)
            {
                bet = 0;
                return;
            }

            bet = Convert.ToInt32(input.text);
        }
    }
}
