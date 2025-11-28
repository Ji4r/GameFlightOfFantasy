using System;
using System.Reflection;
using UnityEngine;

namespace DiplomGames
{
    public class STEntryPoint : MonoBehaviour
    {
        private DIContainer container;

        [Header("Scene Dependencies")]
        [SerializeField] private STColorValidator colorValidator;
        [SerializeField] private STHistoryColor historyColor;
        [SerializeField] private STSimonWheel simonWheel;
        [SerializeField] private STUiView uiView;

        private void Awake()
        {
            container = new DIContainer();

            RegisterDependencies();

            InjectDependencies();

            InitializeSystem();
        }

        private void RegisterDependencies()
        {
            container.RegisterInstance<DIContainer>(container);

            container.RegisterInstance<STColorValidator>(colorValidator);
            container.RegisterInstance<STHistoryColor>(historyColor);
            container.RegisterInstance<STSimonWheel>(simonWheel);
            container.RegisterInstance<STUiView>(uiView);

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
                            Debug.Log($"Injected {field.FieldType.Name} into {type.Name}.{field.Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to inject {field.FieldType.Name} into {type.Name}.{field.Name}: {ex.Message}");
                    }
                }
            }

            // Вызываем метод инициализации если есть
            var initializeMethod = type.GetMethod("Initialize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            initializeMethod?.Invoke(target, null);
        }

        private void InitializeSystem()
        {
            Debug.Log("DI System initialized successfully!");
        }
    }
}
