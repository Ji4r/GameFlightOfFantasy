using UnityEngine;
using System.Collections.Generic;

namespace DiplomGames
{
    public class STObjectPool<T> where T : MonoBehaviour
    {
        public T obj { get; }
        public int sizePool { get; }
        private bool autoExpand;
        private Transform container;

        private List<T> pool;

        public STObjectPool(T obj, int sizePool, Transform parentContainer, bool autoExpend = false)
        {
            this.obj = obj;
            this.container = parentContainer;
            this.sizePool = sizePool;
            this.autoExpand = autoExpend;

            CreatePool(sizePool);
        }

        public STObjectPool(T obj, int sizePool, bool autoExpend = false)
        {
            this.obj = obj;
            this.container = null;
            this.sizePool = sizePool;
            this.autoExpand = autoExpend;

            CreatePool(sizePool);
        }

        private void CreatePool(int size)
        {
           pool = new List<T>();

            for (int i = 0; i < size; i++)
            {
                CreateObject();
            }
        }


        private T CreateObject(bool isActiveInHierarchy = false)
        {
            var newObject = Object.Instantiate(obj, container);
            pool.Add(newObject);
            newObject.gameObject.SetActive(isActiveInHierarchy);
            return newObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (var item in pool)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    element = item;
                    element.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

       public T GetFreeElement()
       {
            if (HasFreeElement(out var element))
            {
                return element;
            }

            if (autoExpand)
            {
                return CreateObject(true);
            }

            throw new System.Exception($"Пул закончился и он не авто расширяемый {typeof(T)}");
       }
    }
}
