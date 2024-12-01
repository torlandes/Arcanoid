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
        [SerializeField] private Button _buttonInfo;
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private GameObject _scrollView;
        [SerializeField] private GameObject _infoContent;
        
        // private int _selectedLevel = -1;
        
        #endregion

        #region Unity lifecycle
        
        private void Awake()
        {
            _buttonStart.onClick.AddListener(StartButtonClickedCallback);
            _buttonLevels.onClick.AddListener(LevelsButtonClickedCallback);
            _buttonInfo.onClick.AddListener(InfoButtonClickedCallback);
            _buttonBack.onClick.AddListener(BackButtonClickedCallback);
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
        private void InfoButtonClickedCallback()
        {
            _buttons.SetActive(false);
            _infoContent.SetActive(true);
        }   
        
        private void BackButtonClickedCallback()
        {
            _buttons.SetActive(true);
            _infoContent.SetActive(false);
        }   
        private void ExitButtonClickedCallback()
        {
            SceneLoaderService.Instance.ExitGame();
        }

        #endregion
    }
}