using UnityEngine;

namespace DiplomGames
{
    public class MEntryPoint : EntryPoint<MEntryPoint>
    {
        [SerializeField] private VetrickControll vetrickControll;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<EntryPoint>(this);
        }
    }
}
