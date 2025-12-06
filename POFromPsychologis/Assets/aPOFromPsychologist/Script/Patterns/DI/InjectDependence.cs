using System;
using System.Reflection;
using UnityEngine;

namespace DiplomGames
{
    public abstract class InjectDependence : MonoBehaviour
    {
        protected DIContainer container;

        protected virtual void StartInjectDependencies()
        {
            RegisterDependencies();

            InjectDependencies();

            InitializeSystem();
        }

        protected abstract void RegisterDependencies();

        public virtual void InjectDependencies()
        {
            var allBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

            foreach (var behaviour in allBehaviours)
            {
                InjectDependenciesInto(behaviour);
            }
        }

        public virtual void InjectDependenciesInto(object target)
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

        public virtual void InitializeSystem()
        {
            Debug.Log("DI System initialized successfully!");
        }
    }
}
