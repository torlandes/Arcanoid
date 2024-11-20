using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PickUp : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _pickUpVfxPrefab;
        [SerializeField] private AudioClip _pickUpAudioClip;
        [SerializeField] private int _score;

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

        #region Protected methods

        protected virtual void PerformActions()
        {
            AudioService.Instance.PlaySfx(_pickUpAudioClip);
            GameService.Instance.AddScore(_score);
            if (_pickUpVfxPrefab != null)
            {
                Instantiate(_pickUpVfxPrefab, transform.position, Quaternion.identity);
            }
        }

        #endregion
    }
}