using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class ChangePlatformSizePickUp : PickUp
    {
        #region Variables

        [Header(nameof(ChangePlatformSizePickUp))]
        [SerializeField] private float _sizeChange;
        private float _x;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            Platform platform = FindObjectOfType<Platform>();
            platform.transform.localScale *= 1 + _sizeChange / 100;
        }

        #endregion
    }
}