using UnityEngine;

namespace DiplomGames
{
    public class GlobalDI : MonoBehaviour
    {
        private DIContainer container;
        private DataSettings dataSetings;
        private SaveSystem saveSystem;

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
            
            saveSystem = new SaveSystem();

            container.RegisterInstance<ISaveSystems>(saveSystem);
            container.RegisterInstance<SaveDataSettings>(new SaveDataSettings("Perfomanse.fof", container.Resolve<ISaveSystems>()));
            // тут дальше будет инициализация службы сохрания и т.д
        }
    }
}
