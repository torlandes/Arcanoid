using System;
using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [Header("Block Settings")]
        [SerializeField] private int _score = 1;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _spriteLowCrackedBlock;
        [SerializeField] private Sprite _spriteHardCrackedBlock;
        [SerializeField] private int _life;
        [SerializeField] private bool _isInvisible;

        [Header("Explosive")]
        [SerializeField] private bool _isExplosive;
        [SerializeField] private float _explosiveRadius = 1f;
        [SerializeField] private LayerMask _explosiveLayerMask;
        [SerializeField] private GameObject _explosionVfxPrefab;
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Events

        public static event Action<Block> OnCreated;
        public static event Action<Block> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _spriteRenderer.enabled = !_isInvisible;
            OnCreated?.Invoke(this);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionImpact();

            if (_life > 0)
            {
                _life--;
                UpdateBlockSprite();
            }

            if (_life <= 0)
            {
                DestroyBlock();
            }

            if (!_isInvisible)
            {
                return;
            }

            _spriteRenderer.enabled = true;
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
            OnCollisionImpact();
            Explode();
            
            OnDestroyed?.Invoke(this);
        }

        private void Explode()
        {
            if (!_isExplosive)
            {
                return;
            }

            OnCollisionImpact();

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

        private void OnCollisionImpact()
        {
            AudioService.Instance.PlaySfx(_explosionAudioClip);
            Instantiate(_explosionVfxPrefab, transform.position, Quaternion.identity);
        }

        private void UpdateBlockSprite()
        {
            if (_life == 2)
            {
                _spriteRenderer.sprite = _spriteLowCrackedBlock;
            }

            if (_life == 1)
            {
                _spriteRenderer.sprite = _spriteHardCrackedBlock;
            }
        }

        #endregion
    }
}