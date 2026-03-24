using UnityEngine;

namespace DiplomGames
{
    public class M2EntryPoint : EntryPoint<M2EntryPoint>
    {
        [SerializeField] private VetrickControll vetrickControll;
        [SerializeField] private M2Resources resources;
        [SerializeField] private M2GameManager gameManager;
        [SerializeField] private PlayPhrasesVetricksOnCall playPhrase;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<M2Resources>(resources);
            container.RegisterInstance<M2GameManager>(gameManager);
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
