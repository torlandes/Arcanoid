using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class ChangeBallSizePickUp : PickUp
    {
        #region Variables

        [Header(nameof(ChangeBallSizePickUp))]
        [SerializeField] private float _sizeChange;
        [SerializeField] private GameObject _ball;
        

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            
            _ball.transform.localScale *=  1 + _sizeChange / 100;

            // Ball ball = LevelService.Instance.Ball;
            // if (ball != null)
            // {
            //     ball.transform.localScale *= 1 + _sizeChange / 100;
            // }
        }

        #endregion
    }
}