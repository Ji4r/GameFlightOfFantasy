using UnityEngine;

namespace DiplomGames
{
    public class MEntryPoint : EntryPoint
    {
        public static MEntryPoint Instance;

        private LoadScreenManager manager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this.gameObject);
        }

        public override void Initialized(DIContainer parentContainer = null)
        {
            container = new DIContainer(parentContainer);
            manager = container.Resolve<LoadScreenManager>();

            if (manager == null)
            {
                Debug.Log("Manager is null in di");
                return;
            }

            manager.HideLoadScreenAndShowAnims();
        }

        public async void LoadScene(int indexScene)
        {
            await manager?.ActiveLoadScreenAndShowAnims();
            await SwitchScene.SwitchSceneByIdAsyncStatic(indexScene);

            var listEntryPoint = SearchEntryPoint();

            foreach (var entryPoint in listEntryPoint)
            {
                entryPoint.Initialized(container);
            }
       
            Destroy(this.gameObject);
        }

        public override void InjectDependencies()
        {
            base.InjectDependencies();
        }

        public override void InjectDependenciesInto(object target)
        {
            base.InjectDependenciesInto(target);
        }

        public override void InitializeSystem()
        {
            base.InitializeSystem();
        }


        public override EntryPoint[] SearchEntryPoint()
        {
            return base.SearchEntryPoint();
        }

        protected override void RegisterDependencies()
        {
            
        }
    }
}
