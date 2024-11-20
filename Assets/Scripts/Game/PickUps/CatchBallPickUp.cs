using Arcanoid.Services;

namespace Arcanoid.Game.PickUps
{
    public class CatchBallPickUp : PickUp
    {
        protected override void PerformActions()
        {
            base.PerformActions();
            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.ResetBall();
            }
        }

    }
}