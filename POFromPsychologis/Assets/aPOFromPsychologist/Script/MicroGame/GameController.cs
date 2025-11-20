using UnityEngine;

namespace DiplomGames
{
    public abstract class GameController : MonoBehaviour
    {
        protected virtual void StartGame() { }

        protected virtual void NextRound() { }

        protected virtual void EndGame() { }

        protected virtual void RestartGame() { }
    }
}
