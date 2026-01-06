using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DiplomGames
{
    public class SaveDataSettings
    {
        private readonly string nameFile;
        private DataSettings baseSettings;
        private ISaveSystems saveSystems;
        private AsyncOperationHandle<ScriptableSettings> settingsHandle;

        public SaveDataSettings(string nameFile, ISaveSystems system) 
        {
            this.nameFile = nameFile;
            this.saveSystems = system;
            LoadBaseConfig();
        }

        private async void LoadBaseConfig()
        {
            settingsHandle = Addressables.LoadAssetAsync<ScriptableSettings>("BaseConfig");
            await settingsHandle.Task;

            if (settingsHandle.Status == AsyncOperationStatus.Succeeded)
            {
                var data = settingsHandle.Result.Data;
                data.Clone(out baseSettings);
            }       
        }

        public void Save(DataSettings data, Action<bool> callback = null) 
        {
            saveSystems.Save(nameFile, data, callback);
        }

        public DataSettings Load(Action<bool> callback = null)
        {
            DataSettings data = (DataSettings)saveSystems.Load<DataSettings>(nameFile, loadedData =>
            {
                callback?.Invoke(loadedData != null);
            });

            if (data == null)
            {
                data = baseSettings;

                Save(data, saveSuccess =>
                {
                    callback?.Invoke(saveSuccess);
                });
            }
            else
            {
                callback?.Invoke(true);
            }

            return data;
        }
    }
}
