using System;
using System.Reflection;
using UnityEngine;

namespace DiplomGames
{
    public class STEntryPoint : EntryPoint
    {
        public static STEntryPoint Instance;

        private DIContainer container;
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

            container = new DIContainer();

            RegisterDependencies();

            InjectDependencies();

            InitializeSystem();

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


        private void RegisterDependencies()
        {
            container.RegisterInstance<DIContainer>(container);

            container.RegisterInstance<STColorValidator>(colorValidator);
            container.RegisterInstance<STHistoryColor>(historyColor);
            container.RegisterInstance<STSimonWheel>(simonWheel);
            container.RegisterInstance<STUiView>(uiView);
            container.RegisterInstance<STGameController>(gameController);

            // Регистрируем создаваемые зависимости
            container.RegistationTransient<STAnimsHistoryElement>(c =>
                new STAnimsHistoryElement(0.3f));
        }

        private void InjectDependencies()
        {
            var allBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

            foreach (var behaviour in allBehaviours)
            {
                InjectDependenciesInto(behaviour);
            }
        }

        private void InjectDependenciesInto(object target)
        {
            var type = target.GetType();
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                // ПРОЩЕ: проверяем есть ли атрибут (не создавая массив)
                var injectAttribute = field.GetCustomAttribute<InjectAttribute>();
                if (injectAttribute != null)
                {
                    try
                    {
                        var dependency = container.Resolve(field.FieldType);
                        if (dependency != null)
                        {
                            field.SetValue(target, dependency);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to inject {field.FieldType.Name} into {type.Name}.{field.Name}: {ex.Message}");
                    }
                }
            }

            // Вызываем метод инициализации если есть
            var initializeMethod = type.GetMethod("Init", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            initializeMethod?.Invoke(target, null);
        }

        private void InitializeSystem()
        {
            Debug.Log("DI System initialized successfully!");
            // Вот тут надо бы добавить загрузочный экран!
            //loadScreenManager.HideLoadScreenAndShowAnims();
        }
    }
}
