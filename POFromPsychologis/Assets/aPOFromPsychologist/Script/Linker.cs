using System;
using UnityEngine;

namespace DiplomGames
{
    public class Linker : MonoBehaviour
    {
        public void OpenWebPage()
        {
            Application.OpenURL("https://dalink.to/weitersindie");
        }

        public void CloseUrlAnalyst() 
        {
            Application.OpenURL("https://t.me/stavkaVB");
        }
    }
}
