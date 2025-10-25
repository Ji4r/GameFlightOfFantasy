using UnityEngine;

namespace DiplomGames
{
    public class CheckerSlot : MonoBehaviour
    {
        [SerializeField] private UiViewFS uiViewFS;
        [SerializeField] private FSGameController gameController;
        private AudioClip theRightSound;

        public void UpdateRightSound(AudioClip newRightClip)
        {
            theRightSound = newRightClip;
        }

        public void CheckingSound(Transform objectTrans)
        {
            if (objectTrans.TryGetComponent<SoundPayerButton>(out var sound))
            {
                bool isRightAnswer = sound.GetClip() == theRightSound ? true : false;
                uiViewFS.ShowOtvetSound(isRightAnswer);

                if (isRightAnswer)
                    gameController.StartNextGame?.Invoke();
            }
        }
    }
}
