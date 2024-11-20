﻿using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class ChangeScorePick : PickUp
    {
        #region Variables

        private int _score;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            GameService.Instance.AddScore(_score);
        }

        #endregion
    }
}