using UnityEngine;
using UnityEngine.Events;

namespace DiplomGames
{
    public class AcceptHotkey : MonoBehaviour
    {
        [SerializeField] private UnityEvent acceptEvent;
        [SerializeField] private UnityEvent notAcceptEvent;
    }
}
