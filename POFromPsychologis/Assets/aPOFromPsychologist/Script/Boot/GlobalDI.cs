using UnityEngine;

namespace DiplomGames
{
    public class GlobalDI : MonoBehaviour
    {
        private DIContainer Container;
        private DataSettings dataSetings;
        private SaveSystem saveSystem;

        private void Awake()
        {
            InitializedContainer();
        }

        public DIContainer GetDIContainer()
        {
            return Container;
        }

        public void InitializedContainer()
        {
            if (Container != null)
                return;

            this.Container = new DIContainer();
            
            saveSystem = new SaveSystem();
            Container.RegisterInstance<ISaveSystems>(saveSystem);
            Container.RegisterInstance<SaveDataSettings>
                (new SaveDataSettings("Perfomanse.fof", Container.Resolve<ISaveSystems>()));
        }
    }
}
