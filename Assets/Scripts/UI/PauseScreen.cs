using System.Collections;
using Arcanoid.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Arcanoid.UI
{
    public class PauseScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _contentGameObject;

        [Header("Buttons")]
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _exitButton;

        [Header("Animation")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationTime = 1f;

        private Coroutine _fadeInAnimation;
        private Coroutine _fadeOutAnimation;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _menuButton.onClick.AddListener(MenuButtonClickedCallback);
            _continueButton.onClick.AddListener(ContinueButtonClickedCallback);
            _exitButton.onClick.AddListener(ExitButtonClickedCallback);

            _contentGameObject.SetActive(false);
            _canvasGroup.alpha = 0;
        }

        private void Start()
        {
            PauseService.Instance.OnChanged += PauseChangedCallback;
        }

        private void OnDestroy()
        {
            PauseService.Instance.OnChanged -= PauseChangedCallback;
        }

        #endregion

        #region Private methods

        private void ContinueButtonClickedCallback()
        {
            PauseService.Instance.TogglePause();
        }

        private void ExitButtonClickedCallback()
        {
            SceneLoaderService.Instance.ExitGame();
        }

        private void MenuButtonClickedCallback()
        {
            GameService.Instance.GameRestart();
        }

        private void PauseChangedCallback(bool isPaused)
        {
            if (isPaused)
            {
                if (_fadeOutAnimation != null)
                {
                    StopCoroutine(_fadeOutAnimation);
                }

                _fadeInAnimation = StartCoroutine(PlayFadeInAnimation());
            }
            else
            {
                if (_fadeInAnimation != null)
                {
                    StopCoroutine(_fadeInAnimation);
                }

                _fadeOutAnimation = StartCoroutine(PlayFadeOutAnimation());
            }
        }

        private IEnumerator PlayFadeInAnimation()
        {
            _contentGameObject.SetActive(true);

            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.unscaledDeltaTime / _animationTime;
                yield return null;
            }
        }

        private IEnumerator PlayFadeOutAnimation()
        {

            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.unscaledDeltaTime / _animationTime;
                yield return null;
            }

            _contentGameObject.SetActive(false);
        }

        #endregion
    }
}