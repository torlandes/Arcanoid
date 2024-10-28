using System;
using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int _score = 1;

        [Header("Explosive")]
        [SerializeField] private bool _isExplosive;
        [SerializeField] private float _explosiveRadius = 1f;
        [SerializeField] private LayerMask _explosiveLayerMask;
        [SerializeField] private GameObject _explosionVfxPrefab;
        

        #endregion

        #region Events

        public static event Action<Block> OnCreated;
        public static event Action<Block> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            OnCreated?.Invoke(this);
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            DestroyBlock();
        }

        private void OnDrawGizmos()
        {
            if (_isExplosive)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, _explosiveRadius);
            }
        }

        #endregion

        #region Public methods

        public void ForceDestroy()
        {
            DestroyBlock();
        }

        #endregion

        #region Private methods

        private void DestroyBlock()
        {
            GameService.Instance.AddScore(_score);
            PickUpService.Instance.SpawnPickUp(transform.position);
            Destroy(gameObject);
            Explode();
        }

        private void Explode()
        {
            if (!_isExplosive)
            {
                return;
            }

            if (_explosionVfxPrefab != null)
            {
                Instantiate(_explosionVfxPrefab, transform.position, Quaternion.identity);
            }

            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(transform.position, _explosiveRadius, _explosiveLayerMask);

            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.TryGetComponent(out Block block))
                {
                    block.ForceDestroy();
                }
            }
        }

        #endregion

        // private bool _isHit;
        // private int _maxLives;
        // private UnityAction<int> _onBlockDestroyed;
    }
}