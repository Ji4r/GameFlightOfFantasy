using UnityEngine;

namespace DiplomGames
{
    public class MenuEntryPoint : EntryPoint<MenuEntryPoint>
    {
        [SerializeField] private VetrickControll vetrickControll;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<EntryPoint>(this);
        }
    }
}
