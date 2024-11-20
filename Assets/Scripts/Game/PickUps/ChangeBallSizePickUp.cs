using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class ChangeBallSizePickUp : PickUp
    {
        #region Variables

        [Header(nameof(ChangeBallSizePickUp))]
        [SerializeField] private float _sizeChange;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.transform.localScale *= 1 + _sizeChange / 100;
            }
        }

        #endregion
    }
}