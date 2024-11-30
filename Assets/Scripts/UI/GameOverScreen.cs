using System.Collections;
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
        
        [Header("Animation")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationTime = 1f;
        
        private Coroutine _fadeInAnimation;
        
        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Instance = this;
            _menuButton.onClick.AddListener(MenuButtonClickedCallback);
            _exitButton.onClick.AddListener(ExitButtonClickedCallback);
            
            _gameOverPanel.SetActive(false);
            _canvasGroup.alpha = 0;
        }

        #endregion

        #region Public methods

        public void ShowGameOver()
        {
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
                _gameOverLabel.text = "GAME OVER!\nTRY AGAIN!";
                _scoreLabel.text = $"Total score: {GameService.Instance.Score}";
                AudioService.Instance.PlaySfx(_overAudioClip);
                PauseService.Instance.TogglePause();
                
                _fadeInAnimation = StartCoroutine(PlayFadeInAnimation());
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
            GameService.Instance.GameRestart();
        }
        
        private IEnumerator PlayFadeInAnimation()
        {
            _gameOverPanel.SetActive(true);

            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.unscaledDeltaTime / _animationTime;
                yield return null;
            }
        }

        #endregion
    }
}