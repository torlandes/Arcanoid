using Arcanoid.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _menuButton.onClick.AddListener(MenuButtonClickedCallback);
            _continueButton.onClick.AddListener(ContinueButtonClickedCallback);
            _exitButton.onClick.AddListener(ExitButtonClickedCallback);
        }

        private void MenuButtonClickedCallback()
        {
            // GameService.Instance.GameRestart();
            // SceneLoaderService.Instance.LoadFirstLevel();
            
            // PauseService.Instance.TogglePause();
            // GameService.Instance.GameRestart();
            // SceneManager.LoadScene(0);
            
            PauseService.Instance.TogglePause();
            GameService.Instance.GameRestart();
            SceneManager.LoadScene("StartScene");
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
        private void PauseChangedCallback(bool isPaused)
        {
            if (_contentGameObject != null)
            {
                _contentGameObject.SetActive(isPaused);
            }

        }
        
        #endregion
    }
}