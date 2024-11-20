using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game
{
    public class DeathZone : MonoBehaviour
    {
        #region Variables

        [SerializeField] private bool _isActive = true;

        #endregion

        #region Unity lifecycle

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isActive)
            {
                return;
            }

            if (other.gameObject.CompareTag(Tag.Ball) && LevelService.Instance.IsLastBall())
            {
                GameService.Instance.ChangeLife(-1);

                if (GameService.Instance.IsGameOver)
                {
                    Destroy(LevelService.Instance.GetFirstBall().gameObject);
                    return;
                }

                LevelService.Instance.GetFirstBall().ResetBall();
                return;
            }
            
            if (other.gameObject.CompareTag(Tag.Ball))
            {
                Destroy(other.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }

        #endregion
    }
}