using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomGames
{
    public abstract class EntryPoint<T> : EntryPoint where T : EntryPoint<T>
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this.gameObject);
        }

        public override void ReloadSceneClearInstance(int indexScene)
        {
            Instance = null;
            LoadScene(indexScene);
        }
    }

    public abstract class EntryPoint : InjectDependence
    {
        protected LoadScreenManager manager;

        public virtual void Initialized(DIContainer parentContainer = null)
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


        public virtual async void LoadScene(int indexScene)
        {
            await manager?.ActiveLoadScreenAndShowAnims();
            await SwitchScene.SwitchSceneByIdAsyncStatic(indexScene);

            var listEntryPoint = SearchEntryPoint(this.gameObject);

            foreach (var entryPoint in listEntryPoint)
            {
                entryPoint.Initialized(container);
            }
  
            Destroy(this.gameObject);
        }

        public virtual void ReloadSceneClearInstance(int indexScene)
        {
           LoadScene(indexScene);
        }

        public virtual EntryPoint[] SearchEntryPoint(GameObject ignoreObject)
        {
            var allEntryPoints = GameObject.FindObjectsByType<EntryPoint>(FindObjectsSortMode.None);

            if (ignoreObject == null)
                return allEntryPoints;

            var filteredList = new List<EntryPoint>();

            foreach (var entryPoint in allEntryPoints)
            {
                if (entryPoint.gameObject == ignoreObject)
                    continue;
                filteredList.Add(entryPoint);
            }

            return filteredList.ToArray();
        }
    }
}
