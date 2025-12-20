using UnityEngine;

namespace DiplomGames
{
    public class FSCheckerSlot : CheckerSlot
    {
        [SerializeField] private UiViewFS uiViewFS;
        [SerializeField] private FSGameController gameController;
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private FirecrackerControllerParticle firecrackerParticle;

        private AudioClip theRightSound;

        public void UpdateRightSound(AudioClip newRightClip)
        {
            theRightSound = newRightClip;
        }

        public override async void CheckRightAnswer(Transform objectTrans)
        {
            if (!objectTrans.TryGetComponent<SoundPayerButton>(out var sound))
                return;

            bool isRightAnswer = sound.GetClip() == theRightSound ? true : false;
            uiViewFS.ShowAnswerSound(isRightAnswer);

            if (isRightAnswer)
            {
                sound.StopPlay();
                SoundPlayer.instance.PlaySound(ListSound.answerSuccesful);
                gameController.StartNextGame?.Invoke();
                firecrackerParticle.Enabled(true, true);
            }
            else
            {
                sound.StopPlay();
                SoundPlayer.instance.PlaySound(ListSound.answerNotSuccesful);

                await slotManager.StartShake(sound.transform);      
            }
        }
    }
}
