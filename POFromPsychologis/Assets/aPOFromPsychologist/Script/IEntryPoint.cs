using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public interface IEntryPoint 
    {
        public abstract void Initialized(DIContainer parentContainer = null);

        public virtual List<IEntryPoint> SearchEntryPoint()
        {
            var gameManager = new GameObject();
            return gameManager.FindGameObjectsByInterface<IEntryPoint>();
        }
    }
}
