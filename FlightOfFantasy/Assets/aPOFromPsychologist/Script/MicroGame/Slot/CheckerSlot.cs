using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    public abstract class CheckerSlot : MonoBehaviour
    {
        public abstract void CheckRightAnswer(Transform objectTrans);
        
    }
}
