using UnityEngine;

namespace DiplomGames
{
    public class GlobalDI : MonoBehaviour
    {
        private DIContainer container;

        private void Awake()
        {
            InitializedContainer();
        }

        public DIContainer GetDIContainer()
        {
            return container;
        }

        public void InitializedContainer()
        {
            if (container != null)
                return;

            this.container = new DIContainer();

            // тут дальше будет инициализаци€ служб сохрани€ и т.д
        }
    }
}
