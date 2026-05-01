using UnityEngine;

namespace DiplomGames
{
    public class APBuilderMap : MonoBehaviour
    {
        [SerializeField] private byte columnCount;
        [SerializeField] private byte rowsCount;

        private APLevelGenerator apLevelGenerator;

        private void Start()
        {
            apLevelGenerator = new APLevelGenerator(columnCount,rowsCount);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                apLevelGenerator.GenerateNewLevel();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                apLevelGenerator.PrintMap();
            }
        }
    }
}
