using UnityEngine;

namespace Arcanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Color _color;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        // [SerializeField] private int _lives = 1;

        private bool _isHit;

        #endregion

        #region Unity lifecycle

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isHit)
            {
                _isHit = true;
                _spriteRenderer.color = _color;
                return;
            }

            Destroy(gameObject);
        }

        #endregion
    }
}