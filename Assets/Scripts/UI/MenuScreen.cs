using System;
using Arcanoid.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arcanoid.UI
{
    public class LevelSelectionScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonLevels;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private GameObject _scrollView;
        
        // private int _selectedLevel = -1;
        
        #endregion

        #region Unity lifecycle
        
        private void Awake()
        {
            _buttonStart.onClick.AddListener(StartButtonClickedCallback);
            _buttonLevels.onClick.AddListener(LevelsButtonClickedCallback);
            _buttonExit.onClick.AddListener(ExitButtonClickedCallback);
        }
        
        private void StartButtonClickedCallback()
        {
            SceneLoaderService.Instance.LoadFirstLevel();
        }

        #endregion

        #region Private methods

        private void LevelsButtonClickedCallback()
        {
            _buttons.SetActive(false);
            _scrollView.SetActive(true);
        }        
        
        private void ExitButtonClickedCallback()
        {
            SceneLoaderService.Instance.ExitGame();
        }

        #endregion
    }
}