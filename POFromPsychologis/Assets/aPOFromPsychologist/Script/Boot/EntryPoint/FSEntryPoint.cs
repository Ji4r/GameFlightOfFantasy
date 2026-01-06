using UnityEngine;

namespace DiplomGames
{
    public class FSEntryPoint : EntryPoint<FSEntryPoint>
    {
        [SerializeField] private VetrickControll vetrickControll;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<EntryPoint>((EntryPoint)this);
        }
    }
}
