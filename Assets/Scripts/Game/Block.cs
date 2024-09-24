using System;
using Arcanoid.Services;
using UnityEngine;
using UnityEngine.Events;

namespace Arcanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int _score = 1;
        
        // [SerializeField] private Color _color;
        // [SerializeField] private SpriteRenderer _spriteRenderer;
        // [SerializeField] private GameObject[] _damageStates;
        // [SerializeField] private int _points = 100;
        // [SerializeField] private int _lives = 1;

        private bool _isHit;
        private int _maxLives;
        private UnityAction<int> _onBlockDestroyed;

        #endregion

        #region Events

        public static event Action<Block> OnCreated;
        public static event Action<Block> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            OnCreated?.Invoke(this);
            
            // _maxLives = _lives;
            // UpdateDamageState();
        }
        
        private void OnDestroy()
        {
            GameService.Instance.AddScore(_score);
            OnDestroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
            
            // if (!_isHit)
            // {
            //     _isHit = true;
            //     _spriteRenderer.color = _color;
            //     return;
            // }
            //
            // DestroyBlock();
        }
        

        //
        // #endregion
        //
        // #region Private methods
        //
        // private void DestroyBlock()
        // {
        //     _onBlockDestroyed?.Invoke(_points);
        //     Destroy(gameObject);
        // }
        //
        // private void HandleHit()
        // {
        //     _lives--;
        //     UpdateDamageState();
        //     if (_lives <= 0)
        //     {
        //         GameManager.Instance.AddScore(_points);
        //         Destroy(gameObject);
        //     }
        // }
        //
        // private void UpdateDamageState()
        // {
        //     int stateIndex = Mathf.Clamp(_maxLives - _lives, 0, _damageStates.Length - 1);
        //
        //     foreach (GameObject state in _damageStates)
        //     {
        //         state.SetActive(false);
        //     }
        //
        //     if (_damageStates.Length > stateIndex)
        //     {
        //         _damageStates[stateIndex].SetActive(true);
        //     }
        //     else
        //     {
        //         Debug.LogError("Damage state index out of range.");
        //     }
        // }

        #endregion
    }
}