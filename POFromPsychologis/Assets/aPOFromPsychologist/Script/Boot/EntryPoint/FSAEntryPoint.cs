using UnityEngine;

namespace DiplomGames
{
    public class FSAEntryPoint : EntryPoint<FSAEntryPoint>
    {
        [SerializeField] private VetrickControll vetrickControll;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<EntryPoint>(this);
        }
    }
}
