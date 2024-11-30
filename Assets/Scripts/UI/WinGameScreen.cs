using System.Collections;
using Arcanoid.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

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
        [SerializeField] private Button _nextLevelButton;
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
            _nextLevelButton.onClick.AddListener(NextLevelButtonClickedCallback);
            _menuButton.onClick.AddListener(MenuButtonClickedCallback);
            _exitButton.onClick.AddListener(ExitButtonClickedCallback);
            
            _winPanel.SetActive(false);
            _canvasGroup.alpha = 0;
        }

        #endregion

        #region Public methods

        public void ShowWinScreen()
        {
            if (_winPanel != null)
            {
                Debug.Log("WTF");
                _winPanel.SetActive(true);
                _winLabel.text = "CONGLATURATION!! \nEAT PILLOW!";
                _scoreLabel.text = $"\nTotal score: {GameService.Instance.Score}";
                AudioService.Instance.PlaySfx(_winAudioClip);
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

        private void NextLevelButtonClickedCallback()
        {
            PauseService.Instance.TogglePause();
            SceneLoaderService.Instance.LoadNextLevel();
        }
        
        private IEnumerator PlayFadeInAnimation()
        {
            _winPanel.SetActive(true);

            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.unscaledDeltaTime / _animationTime;
                yield return null;
            }
        }


        #endregion
    }
}