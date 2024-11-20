using Arcanoid.Game;
using Arcanoid.Game.PickUps;
using Arcanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeBallSpeedPickUp : PickUp
    {
        #region Variables

        [Header(nameof(ChangeBallSpeedPickUp))]
        [SerializeField] private float _speedChange;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.ChangeSpeed(_speedChange);
            }
        }

        #endregion
    }
}