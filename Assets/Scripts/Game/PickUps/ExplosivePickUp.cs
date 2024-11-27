using System;
using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class ExplosivePickUp : PickUp
    {
        #region Variables

        [Header(nameof(ExplosivePickUp))]
        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private Sprite _ballSprite;
        [SerializeField] private Gradient _ballCGradient;

        #endregion

        #region Events

        // public event Action<bool> OnSwap;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            foreach (Ball ball in LevelService.Instance.Balls)
            {
                ball.MakeExplosive(_explosionRadius, _ballSprite, _ballCGradient);
            }
        }

        #endregion
    }
}