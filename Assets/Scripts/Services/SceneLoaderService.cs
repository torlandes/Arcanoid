using System.Collections;
using Arcanoid.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arcanoid.Services
{
    public class SceneLoaderService : SingletonMonoBehaviour<SceneLoaderService>
    {
        #region Variables

        [SerializeField] private string[] _levelSceneNames;

        private int _currentSceneIndex;
        private bool _isLoadingNextScene;

        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();

            DetectCurrentSceneIndex();
        }

        #endregion

        #region Public methods
        
        public void ReloadCurrentScene()
        {
            if (_isLoadingNextScene)
            {
                return;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public bool HasNextLevel()
        {
            return _levelSceneNames.Length > _currentSceneIndex + 1;
        }

        public void LoadFirstLevel()
        {
            if (_isLoadingNextScene)
            {
                return;
            }

            _currentSceneIndex = 0;
            LoadCurrentScene();
        }

        public void LoadLevel(int index)
        {
            if (index >= 0 && index < _levelSceneNames.Length)
            {
                _currentSceneIndex = index;
                LoadCurrentScene();
            }
        }

        public void LoadNextLevel()
        {
            if (_isLoadingNextScene)
            {
                return;
            }
            _currentSceneIndex++;
            LoadCurrentScene();
        }

        public void LoadNextLevelWithDelay(float delay)
        {
            if (_isLoadingNextScene)
            {
                return;
            }

            StartCoroutine(LoadNextLevelWithDelayInternal(delay));
        }

        #endregion

        #region Private methods

        private void DetectCurrentSceneIndex()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            _currentSceneIndex = -1;

            for (int i = 0; i < _levelSceneNames.Length; i++)
            {
                if (string.Equals(currentSceneName, _levelSceneNames[i]))
                {
                    _currentSceneIndex = i;
                    return;
                }
            }
        }

        private void LoadCurrentScene()
        {
            SceneManager.LoadScene(_levelSceneNames[_currentSceneIndex]);
        }

        private IEnumerator LoadNextLevelWithDelayInternal(float delay)
        {
            _isLoadingNextScene = true;
            yield return new WaitForSeconds(delay);
            _isLoadingNextScene = false;

            LoadNextLevel();
        }
        
        #endregion
    }
}