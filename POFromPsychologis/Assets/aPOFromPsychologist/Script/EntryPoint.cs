using UnityEngine;

namespace DiplomGames
{
    public abstract class EntryPoint : InjectDependence
    {
        public abstract void Initialized(DIContainer parentContainer = null);

        public virtual EntryPoint[] SearchEntryPoint()
        {
            return GameObject.FindObjectsByType<EntryPoint>(FindObjectsSortMode.None);
        }
    }
}
