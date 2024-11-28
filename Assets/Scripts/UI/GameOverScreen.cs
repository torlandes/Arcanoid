using Arcanoid.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arcanoid.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        #region Variables

        public static GameOverScreen Instance;

        [Header("UI")]
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _gameOverLabel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private AudioClip _overAudioClip;

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

        public void ShowGameOver()
        {
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
                _gameOverLabel.text = "GAME OVER!\nTRY AGAIN";
                _scoreLabel.text = $"Total score: {GameService.Instance.Score}";
                AudioService.Instance.PlaySfx(_overAudioClip);
                PauseService.Instance.TogglePause();
            }
        }

        #endregion

        #region Private methods

        private void ExitButtonClickedCallback()
        {
            SceneLoaderService.Instance.ExitGame();
        }
        
        private void MenuButtonClickedCallback()
        {
            PauseService.Instance.TogglePause();
            GameService.Instance.GameRestart();
            SceneManager.LoadScene("StartScene");
        }

        #endregion
    }
}