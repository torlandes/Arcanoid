using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class ChangeLifePickUp : PickUp
    {
        #region Variables

        [Header(nameof(ChangeLifePickUp))]
        [SerializeField] private int _lifeChange;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            GameService.Instance.ChangeLife(_lifeChange);
        }

        #endregion
    }
}