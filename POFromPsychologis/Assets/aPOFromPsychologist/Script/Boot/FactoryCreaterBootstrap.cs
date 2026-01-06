using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DiplomGames 
{
    public class FactoryCreaterBootstrap : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject gameObjectSoundManager;
        [SerializeField] private AssetReferenceGameObject gameObjectMusicManager;
        private GlobalDI globalDi;

        public async Task InstantiateAsync()
        {
            var GlobalDiObject = new GameObject("[GlobalDi]");
            globalDi = GlobalDiObject.AddComponent<GlobalDI>();
            globalDi.InitializedContainer();

            DontDestroyOnLoad(globalDi.gameObject);

            var prefabAssetSound = await gameObjectSoundManager.InstantiateAsync(Vector3.zero, Quaternion.identity, null).Task;
            DontDestroyOnLoad(prefabAssetSound);

            var prefabAssetMusic = await gameObjectMusicManager.InstantiateAsync(Vector3.zero, Quaternion.identity).Task;
            DontDestroyOnLoad(prefabAssetMusic);

            await Task.Yield();
        }


        public DIContainer GetGlobalDi()
        {
            return globalDi.GetDIContainer();
        }
    }
}
