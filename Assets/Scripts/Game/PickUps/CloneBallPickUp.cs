using Arcanoid.Game;
using Arcanoid.Game.PickUps;
using Arcanoid.Services;
using Arcanoid.Utility;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class CloneBallPickUp : PickUp
    {
        #region Variables

        [Header(nameof(CloneBallPickUp))]
        [SerializeField] private int _cloneBall = 1;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            for (int i = 0; i < _cloneBall; i++)
            {
                foreach (Ball ball in LevelService.Instance.Balls)
                {
                    Vector2 velocity = ball.GetRigidBody().velocity;
                    Vector3 position = ball.transform.position;
                    Ball newBall = Instantiate(ball, position + ArcanoidRandom.GetRandomVector3(),
                        Quaternion.identity);
                    newBall.ForceStart();
                    newBall.GetRigidBody().velocity = velocity + ArcanoidRandom.GetRandomVector2() * velocity.magnitude;
                }
            }
        }

        #endregion
    }
}