using System;
using System.IO;
using UnityEngine;

namespace DiplomGames
{
    public class SaveSystem : ISaveSystems
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);
            try
            {
                var saver = JsonUtility.ToJson(data, true);
                File.WriteAllText(path, saver);
                callback?.Invoke(true);
                Debug.Log($"Сохранено в: {path}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка сохранения: {e.Message}");
                callback?.Invoke(false);
            }
        }

        public object Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                T data = JsonUtility.FromJson<T>(json);

                callback?.Invoke(data);
                return data;
            }

            return null;
        }

        public string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}
