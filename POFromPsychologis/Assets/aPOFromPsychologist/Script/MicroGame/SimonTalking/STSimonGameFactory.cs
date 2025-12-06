using UnityEngine;

namespace DiplomGames
{
    public class STSimonGameFactory : GameObjectFactory
    {
        [SerializeField] private GameObject prefabColor;

        public override GameObject CreateColorButton(Transform parent)
        {
            // Вся логика создания цветной кнопки здесь
            GameObject button = Instantiate(prefabColor, parent);

            // Настраиваем компоненты, если нужно
            if (button.TryGetComponent<STButtonPianino>(out var pianino))
            {
                // Можно добавить стандартную настройку
                //pianino.Initialize();
            }

            return button;
        }
    }
}
