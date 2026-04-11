using UnityEngine;

namespace DiplomGames
{
    public class MEntryPoint : EntryPoint<MEntryPoint>
    {
        [SerializeField] private VetrickControll vetrickControll;
        [SerializeField] private PlayPhrasesVetricksOnCall playPhrase;
        protected override void RegisterDependencies()
        {
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<EntryPoint>(this);
            container.RegisterInstance(playPhrase);
        }
        public override void InitializeSystem()
        {
            playPhrase.PlayWelcomePhrase();
        }
    }
}
