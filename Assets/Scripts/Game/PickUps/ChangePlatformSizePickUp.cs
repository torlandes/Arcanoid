using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class ChangePlatformSizePickUp : PickUp
    {
        #region Variables

        [Header(nameof(ChangePlatformSizePickUp))]
        // [SerializeField] private float _sizeChange;
        private float _x;
        [SerializeField] private float _resizeCoefficient = 1.5f;
        

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            // Platform platform = FindObjectOfType<Platform>();
            // platform.transform.localScale *= 1 + _sizeChange / 100;
            if (LevelService.Instance.Platform == null)
            {
                return;
            }
            _x = 1 + _resizeCoefficient/100;
            LevelService.Instance.Platform.transform.localScale = new Vector3( _x, 1f, 1f);
        }

        #endregion
    }
}