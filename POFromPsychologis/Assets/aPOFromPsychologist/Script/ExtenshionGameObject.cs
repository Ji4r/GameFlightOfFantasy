using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public static class ExtenshionGameObject
    {
        /// <summary>
        /// Находит первый GameObject с компонентом, реализующим указанный интерфейс
        /// </summary>
        public static T FindGameObjectByInterface<T>(this GameObject gameObject) where T : class
        {
            MonoBehaviour[] allMonoBehaviours = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var monoBehaviour in allMonoBehaviours)
            {
                if (monoBehaviour is T interfaceType)
                {
                    return interfaceType;
                }
            }

            return null;
        }

        /// <summary>
        /// Находит GameObjects с компонентом, реализующим указанный интерфейс
        /// </summary>
        public static List<T> FindGameObjectsByInterface<T>(this GameObject gameObject) where T : class
        {
            MonoBehaviour[] allMonoBehaviours = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            List<T> list = new List<T>();

            foreach (var monoBehaviour in allMonoBehaviours)
            {
                if (monoBehaviour is T interfaceType)
                {
                    list.Add(interfaceType);
                }
            }

            return list;
        }
    }
}
