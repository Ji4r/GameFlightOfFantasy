using UnityEngine;

namespace DiplomGames
{
    public class STSimonGameFactory : GameObjectFactory
    {
        [SerializeField] private GameObject prefabColor;

        public override GameObject CreateColorButton(Transform parent)
        {
            GameObject button = Instantiate(prefabColor, parent);

            return button;
        }
    }
}
