using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DiplomGames 
{
    public class FactoryCreaterBootstrap : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject gameObjectSoundManager;
        private GlobalDI globalDi;

        public async Task InstantiateAsync()
        {
            var GlobalDiObject = new GameObject("[GlobalDi]");
            globalDi = GlobalDiObject.AddComponent<GlobalDI>();
            globalDi.InitializedContainer();

            DontDestroyOnLoad(globalDi.gameObject);

            var prefabAssetSound = await gameObjectSoundManager.InstantiateAsync(Vector3.zero, Quaternion.identity, null).Task;
            DontDestroyOnLoad(prefabAssetSound);

            await Task.Yield();
        }

        public DIContainer GetGlobalDi()
        {
            return globalDi.GetDIContainer();
        }
    }
}
