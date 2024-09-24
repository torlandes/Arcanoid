using System;
using Arcanoid.Utility;
using Unity.VisualScripting;
using UnityEngine;

namespace Arcanoid.Services
{
    public class GameService : SingletonMonoBehaviour<GameService>
    {
        #region Variables

        [SerializeField] private int _score;

        #endregion

        #region Properties

        public event Action<int> OnScoreChanged;

        public int Score => _score;

        #endregion

        #region Unity lifecycle

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

        #endregion

        #region Private methods

        private void AllBlocksDestroyedCallback()
        {
            Debug.LogError("WIN!");
            SceneLoaderService.LoadNextLevelTest();
        }

        #endregion
    }
}