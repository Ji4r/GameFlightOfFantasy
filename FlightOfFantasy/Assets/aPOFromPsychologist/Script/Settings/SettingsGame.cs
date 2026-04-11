using Unity.Burst.Intrinsics;
using UnityEngine;

namespace DiplomGames
{
    public class SettingsGame : MonoBehaviour
    {
        private DataSettings dataSettings;
        private SaveDataSettings saveData;

        private AudioSettingsController audioSettingsController;
        private DisplaySettingsController displaySettingsController;

        public AudioSettingsController AudioSettingsController => audioSettingsController;
        public DisplaySettingsController DisplaySettingsController => displaySettingsController;

        

        public void Initialized(SaveDataSettings saver, DataSettings data)
        {
            saveData = saver;
            dataSettings = data;
            
            if (audioSettingsController == null)
                audioSettingsController = new AudioSettingsController(dataSettings);
            if (displaySettingsController == null)
                displaySettingsController = new DisplaySettingsController(dataSettings);
        }

        public void ApplySettings()
        {
            saveData.Save(dataSettings);
        }
    }

    [System.Serializable]
    public class DataSettings
    {
        public float MusicVolume;
        public float SoundVolume;
        public float VoiceVetrickVolume;

        public bool DisplayVetrik;

        public FullScreenMode ScreenMode;
        public CustomResolution ResolutionScreen;

        public void Clone(out DataSettings whoAreCopyingTo)
        {
            whoAreCopyingTo = new();

            whoAreCopyingTo.MusicVolume = this.MusicVolume;
            whoAreCopyingTo.SoundVolume = this.SoundVolume;
            whoAreCopyingTo.VoiceVetrickVolume = this.VoiceVetrickVolume;
            whoAreCopyingTo.DisplayVetrik = this.DisplayVetrik;
            whoAreCopyingTo.ScreenMode = this.ScreenMode;
            whoAreCopyingTo.ResolutionScreen = this.ResolutionScreen;
        }
    }
}
