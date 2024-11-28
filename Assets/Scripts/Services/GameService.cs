﻿using System;
using Arcanoid.UI;
using Arcanoid.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        [Header("Audio Remove Life")]
        [SerializeField] private AudioClip _overAudioClip;

        #endregion

        #region Events

        // public event Action OnGameOver;
        public event Action<int> OnLiveChanged;
        public event Action<int> OnScoreChanged;

        #endregion

        #region Properties

        public bool IsAutoPlay => _isAutoPlay;
        public bool IsGameOver { get; set; }
        public int Lives => _lives;
        public int Score => _score;

        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();
            _lives = _maxLives;
            IsGameOver = false;
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
            if (IsGameOver)
            {
                return;
            }

            _score += value;
            OnScoreChanged?.Invoke(_score);
        }

        public void ChangeLife(int value)
        {
            if (IsGameOver)
            {
                return;
            }

            _lives += value;
            _lives = Mathf.Clamp(_lives, 0, _maxLives);
            OnLiveChanged?.Invoke(_lives);
            AudioService.Instance.PlaySfx(_overAudioClip);
            CheckGameEnd();
        }

        public void GameRestart()
        {
            IsGameOver = false;
            ResetScore();
            ResetLives();
        }

        public void ResetLives()
        {
            _lives = _maxLives;
            OnLiveChanged?.Invoke(0);
        }

        #endregion

        #region Private methods

        private void AllBlocksDestroyedCallback()
        {
            if (SceneLoaderService.Instance.HasNextLevel() && SceneLoaderService.Instance.CheckNoMenu()) //  TODO: This is not fine
            {
                Debug.Log("HAS LEVEEEEEL");
                SceneLoaderService.Instance.LoadNextLevelDelayed();
            }
            else
            {
                Debug.Log("WIN?");
                WinGameScreen.Instance.ShowWinScreen();
            }
        }

        public void CheckGameEnd()
        {
            if (_lives <= 0)
            {
                GameOverScreen.Instance.ShowGameOver();
            }
        }

        private void ResetScore()
        {
            _score = 0;
        }

        #endregion

    }
}