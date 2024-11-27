using System;
using Arcanoid.Utility;
using UnityEngine;

namespace Arcanoid.Services
{
    public class PauseService : SingletonMonoBehaviour<PauseService>
    {
        #region Events

        public event Action<bool> OnChanged;

        #endregion

        #region Properties

        public bool IsPaused { get; private set; }

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnChanged?.Invoke(IsPaused);
                TogglePause();
            }
        }

        #endregion

        #region Private methods

        public void TogglePause()
        {
            Debug.LogError("FUCK!");
            IsPaused = !IsPaused;
            Time.timeScale = IsPaused ? 0 : 1;
        }

        #endregion

    }
}