using Arcanoid.Services;
using TMPro;
using UnityEngine;
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
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Instance = this;
            _restartButton.onClick.AddListener(RestartButtonClickedCallback);
            _exitButton.onClick.AddListener(ExitButtonClickedCallback);
        }

        private void Start()
        {
            UpdateScoreLabel(GameService.Instance.Score);
        }

        #endregion

        #region Public methods

        public void ShowGameOver()
        {
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
                _gameOverLabel.text = $"Game Over!";
                AudioService.Instance.PlaySfx(_overAudioClip);
                PauseService.Instance.TogglePause();
            }
        }

        #endregion

        #region Private methods

        private void UpdateScoreLabel(int score)
        {
            _scoreLabel.text = $"Score: {GameService.Instance.Score}";
        }
        private void RestartButtonClickedCallback()
        {
            SceneLoaderService.Instance.ReloadCurrentScene();
        }        
        private void ExitButtonClickedCallback()
        {
            Application.Quit();
        }

        #endregion
    }
}