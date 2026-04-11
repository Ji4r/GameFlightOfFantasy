using UnityEngine;

namespace DiplomGames
{
    public class FSAEntryPoint : EntryPoint<FSAEntryPoint>
    {
        [SerializeField] private VetrickControll vetrickControll;
        [SerializeField] private PlayPhrasesVetricksOnCall playPhrasesVetricksOnCall;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance(vetrickControll);
            container.RegisterInstance<EntryPoint>(this);
            container.RegisterInstance(playPhrasesVetricksOnCall);
        }

        public override void InitializeSystem()
        {
            playPhrasesVetricksOnCall.PlayWelcomePhrase();
        }
    }
}
