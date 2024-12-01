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
            LevelService.Instance.BlockCountReset();
        }

        #endregion

        #region Public methods

        public void ChooseLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public void LoadFirstLevel()
        {
            _currentSceneIndex = 0;
            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            DetectCurrentSceneIndex();
            _currentSceneIndex++;
            LoadCurrentScene();
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
        
        #endregion
    }
}