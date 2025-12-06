using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DiplomGames
{
    public class EntryPointBoot : InjectDependence
    {
        public static EntryPointBoot Instance;

        [SerializeField] private AssetReferenceGameObject prefabLoadScreen;
        [SerializeField] private RectTransform parentForLoadScreen;
        [SerializeField] private FactoryCreaterBootstrap factory;
        [SerializeField, Tooltip("Айди сцены которую нужно подгрузить")]
        private int indexLoadScene = 0;

        private GameObject loadScreen;

        private async void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this.gameObject);

           await LoadAssets();
        }

        private async Task LoadAssets()
        {
            loadScreen = await prefabLoadScreen.InstantiateAsync(Vector3.zero,Quaternion.identity, null).Task;
            DontDestroyOnLoad(loadScreen);

            await ShowLoadScreen();
        }

        private async Task ShowLoadScreen()
        {
            if (!loadScreen.TryGetComponent<LoadScreenManager>(out var screenManager))
                return;

            screenManager.ActiveLoadScreen();
            await StartLoadProcced(screenManager);
        }

        private async Task StartLoadProcced(LoadScreenManager screenManager)
        {
            screenManager.UpdateTextProcess("Запуск систем");
            await factory.InstantiateAsync();

            DIContainer container = factory.GetGlobalDi();
            container.RegisterInstance<LoadScreenManager>(screenManager);

            screenManager.UpdateTextProcess("Загрузка меню");
            await SwitchScene.SwitchSceneByIdAsyncStatic(indexLoadScene);

            //var newGameObject = new GameObject();
            var entryPoint = GameObject.FindFirstObjectByType<EntryPoint>();
            //var entryPoint = newGameObject.FindGameObjectByInterface<IEntryPoint>();
            entryPoint?.Initialized(container);

            screenManager.UpdateTextProcess(string.Empty);
            Destroy(this.gameObject);
        }

        protected override void RegisterDependencies()
        {

        }


    }
}
