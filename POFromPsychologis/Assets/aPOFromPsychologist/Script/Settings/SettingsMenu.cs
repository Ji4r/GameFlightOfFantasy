using UnityEngine;
using UnityEngine.UI;

namespace DiplomGames
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private SettingMenuItem[] MenuItems;

        private void OnEnable()
        {
            foreach (SettingMenuItem item in MenuItems) 
            {
                if (!item.button) 
                {
                    Debug.LogError("—сыллка на кнопку не устоноалена");
                    continue;
                }
                item.button.onClick.AddListener(() => SwitchMenuItem(item));
            }

            if (MenuItems.Length > 0)
            {
                SwitchMenuItem(MenuItems[0]);
            }
        }

        private void OnDisable()
        {
            foreach (SettingMenuItem item in MenuItems)
            {
                if (!item.button)
                {
                    Debug.LogError("—сыллка на кнопку не устоноалена");
                    continue;
                }
                item.button.onClick.RemoveListener(() => SwitchMenuItem(item));
            }
        }

        private void SwitchMenuItem(SettingMenuItem currentBtn)
        {
            foreach (SettingMenuItem item in MenuItems) 
            {
               item.button.interactable = false;
            }

            HidePanel(MenuItems, currentBtn);

            foreach (SettingMenuItem item in MenuItems)
            {
                if (item != currentBtn)
                    item.button.interactable = true;
            }
        }

        private void HidePanel(SettingMenuItem[] menuItems, SettingMenuItem currentBtn)
        {
            foreach (SettingMenuItem item in menuItems)
            {
                if (currentBtn != item)
                {
                    item.gameObjectItem.SetActive(false);
                }
            }
        }
    }

    [System.Serializable]
    public class SettingMenuItem
    {
        public Button button;
        public GameObject gameObjectItem;
    }
}
