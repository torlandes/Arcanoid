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
        
        [SerializeField] private float _newLevelDelay = 0.5f;

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
        
        public bool HasNextLevel()
        {
            return _levelSceneNames.Length > _currentSceneIndex + 1;
        }

        public void LoadFirstLevel()
        {
            SceneManager.LoadScene(1);
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
            _currentSceneIndex++;
            LoadCurrentScene();
        }

        public void LoadNextLevelDelayed()
        {
            StartCoroutine(LoadNextLevelDelayedInternal());
        }
        
        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
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

        private IEnumerator LoadNextLevelDelayedInternal()
        {
            yield return new WaitForSeconds(_newLevelDelay);
            LoadNextLevel();
        }

        #endregion
        
        public bool CheckedMenuScene()
        {
            return _currentSceneIndex == 0;
        }
        
    }
}