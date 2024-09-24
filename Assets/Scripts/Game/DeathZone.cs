using System;
using Arcanoid.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arcanoid.Game
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private bool _isActive = true;
        
        #region Private methods

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isActive)
            {
                return;
            }
            
            SceneLoaderService.Instance.ReloadCurrentScene();
        }

        #endregion
    }
}