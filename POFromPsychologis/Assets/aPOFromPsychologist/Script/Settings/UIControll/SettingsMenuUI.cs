using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace DiplomGames
{
    public class SettingsMenuUI : MonoBehaviour
    {
        [Header("—сылки на элементы")]
        [SerializeField] private Slider sliderMusic;
        [SerializeField] private Slider sliderSound;
        [SerializeField] private Slider sliderVetrickVoice;

        [SerializeField] private TextMeshProUGUI volumeProcentMusic;
        [SerializeField] private TextMeshProUGUI volumeProcentSound;
        [SerializeField] private TextMeshProUGUI volumeProcentVetrickVoice;
        [SerializeField] private Button btnCloseAndSave; 

        [Header("ќкно настроек экрана")]
        [SerializeField] private TMP_Dropdown displayMode;
        [SerializeField] private GameObject panelSizeWindow;
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        [SerializeField] private Toggle showVetrick;

        [Inject] SaveDataSettings saver;
        [Inject] DataSettings dataSettings;
        [Inject] VetrickControll vetrickControll;

        [SerializeField] private SettingsGame settingsGame;
        [SerializeField] private AudioSettingsController audioSettings;
        [SerializeField] private DisplaySettingsController displaySettings;

        private void Init()
        {
            settingsGame.Initialized(saver, dataSettings);

            if (settingsGame.AudioSettingsController == null || settingsGame.DisplaySettingsController == null)
                Debug.LogError("AudioSettingsController или DisplaySettingsController в SettingsGame равен null");

            showVetrick.onValueChanged.AddListener(SetActivityVetrick);
            audioSettings = settingsGame.AudioSettingsController;
            displaySettings = settingsGame.DisplaySettingsController;
            ValueChangedMusic(dataSettings.MusicVolume);
            ValueChangedSound(dataSettings.SoundVolume);
            ValueChangedVetrickVoice(dataSettings.VoiceVetrickVolume);
            UpdateToggleShowVetrick(dataSettings.DisplayVetrik);

            sliderMusic.onValueChanged.AddListener(ValueChangedMusic);
            sliderSound.onValueChanged.AddListener(ValueChangedSound);
            sliderVetrickVoice.onValueChanged.AddListener(ValueChangedVetrickVoice);

            InitializedResolution();
            InitializeScreenParameters(dataSettings);
            displayMode.onValueChanged.AddListener(ChangeDisplayMode);
            resolutionDropdown.onValueChanged.AddListener(displaySettings.SetResolution);

            btnCloseAndSave.onClick.AddListener(settingsGame.ApplySettings);
        }

        private void OnDisable()
        {
            if (audioSettings == null)
                Debug.LogError("AudioSettingsController в SettingsGame равен null");

            sliderMusic.onValueChanged.RemoveListener(ValueChangedMusic);
            sliderSound.onValueChanged.RemoveListener(ValueChangedSound);
            sliderVetrickVoice.onValueChanged.RemoveListener(ValueChangedVetrickVoice);
                

            if (displaySettings == null)
                Debug.LogError("AudioSettingsController в SettingsGame равен null");
            
            displayMode.onValueChanged.RemoveListener(ChangeDisplayMode);
            resolutionDropdown.onValueChanged.RemoveListener(displaySettings.SetResolution);


            btnCloseAndSave.onClick.RemoveListener(settingsGame.ApplySettings);
        }

        private void ValueChangedMusic(float volume)
        {
            audioSettings.SetVolumeMusic(volume);
            volumeProcentMusic.text = $"{Mathf.RoundToInt(volume * 100)}%";
            UpdateSliderValue(sliderMusic, volume);
        }

        public void ValueChangedMusicNoSaving(float volume)
        {
            sliderMusic.onValueChanged.RemoveListener(ValueChangedMusic);

            audioSettings.SetVolumeMusicNoSaving(volume);
            volumeProcentMusic.text = $"{Mathf.RoundToInt(volume * 100)}%";
            UpdateSliderValue(sliderMusic, volume);

            sliderMusic.onValueChanged.AddListener(ValueChangedMusic);
        }

        private void ValueChangedSound(float volume)
        {
            audioSettings.SetVolumeSound(volume);
            volumeProcentSound.text = $"{Mathf.RoundToInt(volume * 100)}%";
            UpdateSliderValue(sliderSound, volume);
        }

        private void ValueChangedVetrickVoice(float volume)
        {
            audioSettings.SetVolumeVetrickVoice(volume);
            volumeProcentVetrickVoice.text = $"{Mathf.RoundToInt(volume * 100)}%";
            UpdateSliderValue(sliderVetrickVoice, dataSettings.VoiceVetrickVolume);
        }

        private void UpdateSliderValue(Slider slider, float value)
        {
            slider.value = value;
        }


        private void SetActivityVetrick(bool isActivity)
        {
            displaySettings.SetActivityVetrick(isActivity);
            vetrickControll.SetActivity(isActivity);
        }

        public void ChangeDisplayMode(int id)
        {
            FullScreenMode fullScreen = displaySettings.ChangeDisplayMode(id);

            if (fullScreen == FullScreenMode.Windowed)
            {
                panelSizeWindow.SetActive(true);
            }
            else
            {
                panelSizeWindow.SetActive(false);
            }
        }

        public void ChangeDisplayMode(FullScreenMode mode)
        {
            FullScreenMode fullScreen = mode;
            displaySettings.ChangeDisplayMode(mode);

            if (fullScreen == FullScreenMode.Windowed)
            {
                panelSizeWindow.SetActive(true);
            }
            else
            {
                panelSizeWindow.SetActive(false);
            }
        }


        private void InitializedDisplayMode()
        {
            displayMode.ClearOptions();
            displayMode.AddOptions(displaySettings.InitializedDisplayMode());
        }

        private void InitializedResolution()
        {
            resolutionDropdown.ClearOptions();

            List<Resolution> resolution = displaySettings.InitializedResolution(out var options);

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();

            for (int i = 0; i < resolution.Count; i++)
            {
                if (resolution[i].width == Screen.width && resolution[i].height == Screen.height)
                {
                    resolutionDropdown.SetValueWithoutNotify(i);
                    break;
                }
            }
        }

        private void InitializeScreenParameters(DataSettings data)
        {
            InitializedDisplayMode();

            ChangeDisplayMode(data.ScreenMode);
            displaySettings.SetResolution((int)data.ResolutionScreen.ResolutionIndex);
            displayMode.SetValueWithoutNotify((int)data.ScreenMode == 3 ? 0 : (int)data.ScreenMode);

            SetActivityVetrick(dataSettings.DisplayVetrik);
            resolutionDropdown.SetValueWithoutNotify(data.ResolutionScreen.ResolutionIndex);
        }

        private void UpdateToggleShowVetrick(bool isEnabled)
        {
            showVetrick.isOn = isEnabled;
        }
    }
}
