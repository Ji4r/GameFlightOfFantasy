using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DiplomGames
{
    public class SettingsGame : MonoBehaviour
    {
        [Header("Окно настроек окна")]
        [SerializeField] private TMP_Dropdown displayMode;
        [SerializeField] private GameObject panelSizeWindow;

        [Header("Разрешение экрана")]
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        private Dictionary<FullScreenMode, string> keyDisplayMode;
        private Resolution[] resolution;

        private FullScreenMode curentScreenMode;
        private Resolution currentResolutionScreen;

        private void OnEnable()
        {
            displayMode.onValueChanged.AddListener(ChangeDisplayMode);
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        private void OnDisable()
        {
            displayMode.onValueChanged.RemoveListener(ChangeDisplayMode);
            resolutionDropdown.onValueChanged.RemoveListener(SetResolution);
        }

        private void Awake()
        {
            InitializedDisplayMode();
            InitializedResolution();
        }

        private void ChangeDisplayMode(int id)
        {
            switch (id)
            { 
                case 0:
                    SwitchDisplayMode(FullScreenMode.Windowed);
                    break;
                case 1:
                    SwitchDisplayMode(FullScreenMode.FullScreenWindow);
                    break;
                case 2:
                    SwitchDisplayMode(FullScreenMode.MaximizedWindow);
                    break;
            }
        }

        private void SwitchDisplayMode(FullScreenMode fullScreen)
        {
            Screen.fullScreenMode = fullScreen;
            curentScreenMode = fullScreen;
            if (fullScreen == FullScreenMode.Windowed)
            {
                panelSizeWindow.SetActive(true);
            }
            else
            {
                panelSizeWindow.SetActive(false);
            }
        }

        private void SetResolution(int resolutionIndex)
        {
            Resolution resol = resolution[resolutionIndex];
            currentResolutionScreen = resol;
            Screen.SetResolution(resol.width, resol.height, false);
        }

        private void InitializedDisplayMode()
        {
            keyDisplayMode = new Dictionary<FullScreenMode, string>{
                {FullScreenMode.Windowed, "Оконный"},
                {FullScreenMode.FullScreenWindow, "Безрамочный полноэкранный"},
                {FullScreenMode.MaximizedWindow, "Полный экран"},
            };

            displayMode.ClearOptions();

            List<string> keys = new List<string>();
            foreach (KeyValuePair<FullScreenMode, string> kvp in keyDisplayMode)
            {
                keys.Add(kvp.Value);
            }

            displayMode.AddOptions(keys);
            ChangeDisplayMode(displayMode.value);
        }
        private void InitializedResolution()
        {
            resolutionDropdown.ClearOptions();
            var options = new List<string>();
            resolution = Screen.resolutions;

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolution.Length; i++)
            {
                string option = resolution[i].width + "x" + resolution[i].height;
                options.Add(option);
                if (resolution[i].width == Screen.currentResolution.width && resolution[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();

            for (int i = 0; i < resolution.Length; i++)
            {
                if (resolution[i].width == Screen.width && resolution[i].height == Screen.height)
                {
                    resolutionDropdown.SetValueWithoutNotify(i);
                    break;
                }
            }
        }
    }
}
