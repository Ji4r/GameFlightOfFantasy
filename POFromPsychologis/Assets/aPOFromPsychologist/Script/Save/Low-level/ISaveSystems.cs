using System;
using UnityEngine;

namespace DiplomGames
{
    public interface ISaveSystems
    {
        public void Save(string key, object data, Action<bool> callback = null);
        public void Load<T>(string key, Action<T> callback);
    }
}
