using UnityEngine;

namespace DiplomGames
{
    public class FSEntryPoint : EntryPoint
    {
        public static FSEntryPoint Instance;

        private DIContainer container;
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

        public override EntryPoint[] SearchEntryPoint()
        {
            return base.SearchEntryPoint();
        }
    }
}
