using UnityEngine;

namespace DiplomGames
{
    public class FSAChecketSlot : CheckerSlot
    {
        [SerializeField] private FSAUiView uiView;
        [SerializeField] private FSAGameController gameController;
        [SerializeField] private Transform theRightAnswer;

        public void UpdateRightQuestion(Transform newRight)
        {
           theRightAnswer = newRight;
        }

        public override void CheckRightAnswer(Transform objectTrans)
        {
            bool isRightAnswer = objectTrans == theRightAnswer ? true : false;
            uiView.ShowAnswerText(isRightAnswer);

            if (isRightAnswer)
            {
                SoundPlayer.instance.PlaySound(ListSound.answerSuccesful);
                gameController.StartNextGame?.Invoke();
            }
            else
            {
                SoundPlayer.instance.PlaySound(ListSound.answerNotSuccesful);
            }
        }
    }
}
