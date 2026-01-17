namespace DiplomGames
{
    public class AudioSettingsController
    {
        private DataSettings dataSettings;

        public AudioSettingsController(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;
        }

        public void UpdateDataSettings(DataSettings newDataSettings)
        {
            dataSettings = newDataSettings;
        }


        public void SetVolumeMusic(float volume)
        {
            dataSettings.MusicVolume = volume;
            MusicPlayer.instance?.ChangeValueMusic(volume);
        }

        public void SetVolumeMusicNoSaving(float volume)
        {
            MusicPlayer.instance?.ChangeValueMusic(volume);
        }

        public void SetVolumeSound(float volume)
        {
            dataSettings.SoundVolume = volume;
            SoundPlayer.instance.SetVolumeSong(volume);
        }

        public void SetVolumeVetrickVoice(float volume)
        {
            dataSettings.VoiceVetrickVolume = volume;
            SoundVetrickVoice.instance.SetVolumeSong(volume);
        }
    }
}
