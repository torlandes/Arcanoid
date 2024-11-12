using System;
using Arcanoid.UI;
using Arcanoid.Utility;
using UnityEngine;

namespace Arcanoid.Services
{
    public class GameService : SingletonMonoBehaviour<GameService>
    {
        #region Variables

        [Header("Auto Play")]
        [SerializeField] private bool _isAutoPlay;

        [Header("Settings")]
        [SerializeField] private int _maxLives = 3;

        [Header("Stats")]
        [SerializeField] private int _score;
        [SerializeField] private int _lives;

        #endregion

        #region Events

        public event Action<int> OnLiveChanged;
        public event Action<int> OnScoreChanged;

        #endregion

        #region Properties

        public bool IsAutoPlay => _isAutoPlay;
        public int Score => _score;
        public int Lives => _lives;

        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();

            _lives = _maxLives;
        }

        private void Start()
        {
            LevelService.Instance.OnAllBlocksDestroyed += AllBlocksDestroyedCallback;
        }

        private void OnDestroy()
        {
            LevelService.Instance.OnAllBlocksDestroyed -= AllBlocksDestroyedCallback;
        }

        #endregion

        #region Public methods

        public void AddScore(int value)
        {
            _score += value;
            OnScoreChanged?.Invoke(_score);
        }

        public void CheckGameEnd()
        {
            if (_lives <= 0)
            {
                Debug.LogError("GAME OVER!");
                GameOverScreen.Instance.ShowGameOver();
            }
        }

        public void RemoveLife(int value)
        {
            _lives += value;
            _lives = Mathf.Clamp(_lives, 0, _maxLives);
            OnLiveChanged?.Invoke(_lives);
            CheckGameEnd();
        }

        public void ResetBall()
        {
            LevelService.Instance.Ball.ResetBall();
        }

        #endregion

        #region Private methods

        private void AllBlocksDestroyedCallback()
        {
            if (SceneLoaderService.Instance.HasNextLevel())
            {
                SceneLoaderService.Instance.LoadNextLevel();
            }
            else
            {
                Debug.LogError("GAME WIN!");
            }
            // SceneLoaderService.LoadNextLevelTest();
        }

        #endregion
    }
}