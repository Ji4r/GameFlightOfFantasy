using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public class DIContainer : MonoBehaviour
    {
        private readonly DIContainer parentContainer;
        private readonly Dictionary<(string, Type), DIRegistation> registations = new();
        private readonly HashSet<(string, Type)> resolutions = new();

        public DIContainer(DIContainer parentContainer = null) 
        { 
            this.parentContainer = parentContainer;
        }

        public void RegistationSingleton<T>(Func<DIContainer, T> factory)
        {
            RegistationSingleton<T>(null, factory);
        }

        public void RegistationSingleton<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T));
            Registation(key, factory, true);
        }

        public void RegistationTransient<T>(Func<DIContainer, T> factory)
        {
            RegistationTransient<T>(null, factory);
        }

        public void RegistationTransient<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T));
            Registation(key, factory, false);
        }


        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(null, instance);
        }

        public void RegisterInstance<T>(string tag,T instance)
        {
            var key = (tag, typeof(T));

            if (registations.ContainsKey(key))
            {
                throw new Exception($"Этот ключ - {key} уже зарегистрирова в di");
            }

            registations[key] = new DIRegistation
            {
                Instance = instance,
                IsSengleton = true
            };
        }

        private void Registation<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingleton)
        {
            if (registations.ContainsKey(key))
            {
                throw new System.Exception($"Этот ключ - {key} уже зарегистрирова в di");
            }

            registations[key] = new DIRegistation
            {
                Factory = c => factory,
                IsSengleton = isSingleton,
            };
        }


        public T Resolve<T>(string tag = null) 
        {
            var key = (tag, typeof(T));

            if (resolutions.Contains(key))
            {
                throw new Exception($"Кто то уже пытаеться взять данный объект {key}");           
            }

            resolutions.Add(key);

            try
            {
                if (registations.TryGetValue(key, out var registation))
                {
                    if (registation.IsSengleton)
                    {
                        if (registation.Instance == null && registation.Factory != null)
                        {
                            registation.Instance = registation.Factory(this);
                        }

                        return (T)registation.Instance;
                    }

                    return (T)registation.Factory(this);
                }

                if (parentContainer != null)
                {
                    return parentContainer.Resolve<T>(tag);
                }
            }
            finally 
            {
                resolutions.Remove(key);
            }

            throw new Exception($"Ошибка в di {key}");
        }
    }
}
