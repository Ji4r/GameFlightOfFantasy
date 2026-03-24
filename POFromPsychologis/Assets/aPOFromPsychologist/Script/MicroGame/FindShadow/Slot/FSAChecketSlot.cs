using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DiplomGames
{
    public class FSAChecketSlot : CheckerSlot
    {
        [SerializeField] private FSAUiView uiView;
        [SerializeField] private FSAGameController gameController;
        [SerializeField] private FSASlotManager slotManager;
        [SerializeField] private Transform theRightAnswer;
        [Inject] private PlayPhrasesVetricksOnCall playPhrasesVetricksOnCall;
        public void UpdateRightQuestion(Transform newRight)
        {
           theRightAnswer = newRight;
        }

        public override async void CheckRightAnswer(Transform objectTrans)
        {
            bool isRightAnswer = objectTrans == theRightAnswer ? true : false;
            uiView.ShowAnswerText(isRightAnswer);

            if (isRightAnswer)
            {
                SoundPlayer.instance.PlaySound(ListSound.answerSuccesful);
                gameController.StartNextGame?.Invoke();
                if (playPhrasesVetricksOnCall.ShouldPlayPhrase())
                    await playPhrasesVetricksOnCall.PlayPhraseAndHideVetrick();
            }
            else
            {
                SoundPlayer.instance.PlaySound(ListSound.answerNotSuccesful);
                await playPhrasesVetricksOnCall.PlayPhraseAndHideVetrick(TypePhrase.MotivationalPhrase);
                await slotManager.StartShake(objectTrans);
            }
        } 
    }
}
