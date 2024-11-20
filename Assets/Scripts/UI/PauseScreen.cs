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
        // [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _continueButton.onClick.AddListener(ContinueButtonClickedCallback);
            // _restartButton.onClick.AddListener(RestartButtonClickedCallback);
            _exitButton.onClick.AddListener(ExitButtonClickedCallback);
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
        
        // private void RestartButtonClickedCallback()
        // {
        //     PauseService.Instance.TogglePause();
        //     GameService.Instance.GameRestart();
        // }        
        
        private void ExitButtonClickedCallback()
        {
            SceneLoaderService.Instance.ExitGame();
        }
        private void PauseChangedCallback(bool isPaused)
        {
            _contentGameObject.SetActive(isPaused);
        }
        
        #endregion
    }
}