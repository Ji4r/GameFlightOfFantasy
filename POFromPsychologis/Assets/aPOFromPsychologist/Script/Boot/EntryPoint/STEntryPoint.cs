using UnityEngine;

namespace DiplomGames
{
    public class STEntryPoint : EntryPoint
    {
        public static STEntryPoint Instance;

        private LoadScreenManager manager;
        
        [Header("Scene Dependencies")]
        [SerializeField] private STColorValidator colorValidator;
        [SerializeField] private STGameController gameController;
        [SerializeField] private STHistoryColor historyColor;
        [SerializeField] private STSimonWheel simonWheel;
        [SerializeField] private STUiView uiView;

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

            StartInjectDependencies();

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

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<STColorValidator>(colorValidator);
            container.RegisterInstance<STHistoryColor>(historyColor);
            container.RegisterInstance<STSimonWheel>(simonWheel);
            container.RegisterInstance<STUiView>(uiView);
            container.RegisterInstance<STGameController>(gameController);
            var gameSettingsManager = new STGameSettingsManager();
            container.RegisterInstance<STGameSettingsManager>(gameSettingsManager);

            // Регистрируем создаваемые зависимости
            container.RegistationTransient<STAnimsHistoryElement>(c =>
                new STAnimsHistoryElement(0.3f));
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

        protected override void StartInjectDependencies()
        {
            base.StartInjectDependencies();
        }

        public override EntryPoint[] SearchEntryPoint()
        {
            return base.SearchEntryPoint();
        }
    }
}
