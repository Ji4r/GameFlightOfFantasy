using UnityEngine;

namespace DiplomGames
{
    public class FSEntryPoint : EntryPoint<FSEntryPoint>
    {
        [SerializeField] private float volumeMusic = 0f;
        [SerializeField] private VetrickControll vetrickControll;
        [SerializeField] private SettingsMenuUI uiSettingsMenu;

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
            manager.HideLoadScreenAndShowAnims();
        }

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<EntryPoint>((EntryPoint)this);
            container.RegisterInstance<SettingsMenuUI>(uiSettingsMenu);
        }
    }
}
