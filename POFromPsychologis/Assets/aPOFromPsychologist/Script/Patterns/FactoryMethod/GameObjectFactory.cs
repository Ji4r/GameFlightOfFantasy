using UnityEngine;

namespace DiplomGames
{
    public abstract class GameObjectFactory : MonoBehaviour
    {
       public abstract GameObject CreateColorButton(Transform parent);
    }
}
