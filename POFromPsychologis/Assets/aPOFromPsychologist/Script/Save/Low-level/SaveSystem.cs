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
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);
        }

        public string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}
