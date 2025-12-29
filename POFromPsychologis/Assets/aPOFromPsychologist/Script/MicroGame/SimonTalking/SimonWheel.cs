using UnityEngine;

namespace DiplomGames
{
    public class SimonWheel : MonoBehaviour
    {
        [SerializeField] private Transform referenceOnContents;

        public Transform ReferenceOnContents { get { return referenceOnContents; } }
    }
}
