using UnityEngine;

namespace DiplomGames
{
    [CreateAssetMenu(fileName = "STWheelPrefab", menuName = "ScriptableObjects/SimonSays/WheelPrefab")]
    public class STGamePreset : ScriptableObject
    {
        [SerializeField] private GameObject prefabWheel;
        [SerializeField] private int whatCreateColor;

        public GameObject PrefabWheel { get { return prefabWheel; } }
        public int WhatCreateColor { get { return whatCreateColor; } }
    }
}
