using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PickUp : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _scoreVfxPrefab;
        [SerializeField] private AudioClip _scoreAudioClip;

        #endregion

        #region Unity lifecycle

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tag.Platform))
            {
                PerformActions();
                Destroy(gameObject);
            }
        }

        #endregion

        #region Private methods

        protected virtual void PerformActions()
        {
            AudioService.Instance.PlaySfx(_scoreAudioClip);
            if (_scoreVfxPrefab != null)
            {
                Instantiate(_scoreVfxPrefab, transform.position, Quaternion.identity);
            }
            
        }

        #endregion
    }
}