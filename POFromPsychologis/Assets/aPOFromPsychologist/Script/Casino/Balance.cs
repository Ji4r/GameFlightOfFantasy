using TMPro;
using UnityEngine;

namespace DiplomGames
{
    public class Balance : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMoney;
        public float balance;

        private void Start()
        {
            balance = 1000;
            UpdMoney();
        }


        private void Update()
        {
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.Z) && Input.GetKeyDown(KeyCode.Space))
            {
                balance += 1000;
                UpdMoney();
            }

            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Backspace))
            {
                balance += 1000;
                UpdMoney();
            }
        }

        public void UpdMoney()
        {
            textMoney.text = "Âארט באככû ןמ ÐÏÌ: " + balance;
        }

        public void AddBalance(int addBalance)
        {
            if (addBalance > 0)
            {
                balance += addBalance;
                UpdMoney();
            }
        }
    }
}
