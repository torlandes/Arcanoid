using Arcanoid.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arcanoid.UI
{
    public class WinGameScreen : MonoBehaviour
    {
        #region Variables

        public static WinGameScreen Instance;

        [Header("UI")]
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _winLabel;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private AudioClip _winAudioClip;
        
        [Header("Buttons")]
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _exitButton;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Instance = this;
            _menuButton.onClick.AddListener(MenuButtonClickedCallback);
            _exitButton.onClick.AddListener(ExitButtonClickedCallback);
        }
        
        #endregion

        #region Public methods

        public void ShowWinScreen()
        {
            if (_winPanel != null)
            {
                Debug.Log("WTF");
                _winPanel.SetActive(true);
                _winLabel.text = $"YOU WIN!\nYOU BEST OF THE BEST!";
                _scoreLabel.text = $"Total score: {GameService.Instance.Score}";
                AudioService.Instance.PlaySfx(_winAudioClip);
                PauseService.Instance.TogglePause();
            }
        }

        #endregion

        #region Private methods
        
        private void MenuButtonClickedCallback()
        {
            PauseService.Instance.TogglePause();
            GameService.Instance.GameRestart();
            SceneManager.LoadScene(0);
        }        
        private void ExitButtonClickedCallback()
        {
            SceneLoaderService.Instance.ExitGame();
        }

        #endregion
    }
}