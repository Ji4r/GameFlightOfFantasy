using UnityEngine;

namespace DiplomGames
{
    public class M2EntryPoint : EntryPoint<M2EntryPoint>
    {
        [SerializeField] private M2Resources resources;
        [SerializeField] private M2GameManager gameManager;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<M2Resources>(resources);
            container.RegisterInstance<M2GameManager>(gameManager);
        }
    }
}
