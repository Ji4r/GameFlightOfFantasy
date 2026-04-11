using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "BaseConfigSettings", menuName = "ScriptableObjects/Settings/BaseConfig")]
    public class ScriptableSettings : ScriptableObject
    {
        [SerializeField] private DataSettings data;
        public DataSettings Data { get { return data; }}
    }
}
