using UnityEngine;

namespace DiplomGames
{
    public class FSEntryPoint : EntryPoint<FSEntryPoint>
    {
        [SerializeField] private float volumeMusic = 0f;
        [SerializeField] private PlayPhrasesVetricksOnCall playPhrase;
        [SerializeField] private VetrickControll vetrickControll;
        [SerializeField] private SettingsMenuUI uiSettingsMenu;

        [Header("UI")]
        [SerializeField] private AudioSource soundPlayer;

        public override void Initialized(DIContainer parentContainer = null)
        {
            container = new DIContainer(parentContainer);
            manager = container.Resolve<LoadScreenManager>();

            if (manager == null)
            {
                Debug.Log("Manager is null in di");
                return;
            }

            StartInjectDependencies();
            uiSettingsMenu.ValueChangedMusicNoSaving(volumeMusic);
            soundPlayer.volume = container.Resolve<DataSettings>().SoundVolumeOnGame;
            uiSettingsMenu.ChangeVolumeOnGame += UpdateSoundOnGame;
            manager.HideLoadScreenAndShowAnims();
        }

        private void OnDisable()
        {
            uiSettingsMenu.ChangeVolumeOnGame -= UpdateSoundOnGame;
        }

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<EntryPoint>((EntryPoint)this);
            container.RegisterInstance<SettingsMenuUI>(uiSettingsMenu);
            container.RegisterInstance(playPhrase);
        }

        private void UpdateSoundOnGame(float volume)
        {
            soundPlayer.volume = volume;
        }
    }
}
