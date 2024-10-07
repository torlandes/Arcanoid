using Arcanoid.Services;
using TMPro;
using UnityEngine;

namespace Arcanoid.UI
{
    public class GameScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text _scoreLabel;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            GameService.Instance.OnScoreChanged += ScoreChangedCallback;
            UpdateScore();
        }

        private void OnDestroy()
        {
            GameService.Instance.OnScoreChanged -= ScoreChangedCallback;
        }

        #endregion

        #region Private methods

        private void ScoreChangedCallback(int score)
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            _scoreLabel.text = $"Score: {GameService.Instance.Score}";
        }

        #endregion
    }
}