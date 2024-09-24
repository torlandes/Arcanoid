// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace Arcanoid.Game
// {
//     public class GameManager : MonoBehaviour
//     {
//         #region Variables
//
//         public static GameManager Instance;
//         private int _score;
//
//         #endregion
//
//         #region Properties
//
//         private TMP_Text ScoreText { get; set; }
//
//         #endregion
//
//         #region Unity lifecycle
//
//         private void Awake()
//         {
//             if (Instance == null)
//             {
//                 Instance = this;
//                 DontDestroyOnLoad(gameObject);
//                 SceneManager.sceneLoaded += OnSceneLoaded;
//             }
//             else
//             {
//                 Destroy(gameObject);
//             }
//         }
//
//         private void OnDestroy()
//         {
//             if (Instance == this)
//             {
//                 SceneManager.sceneLoaded -= OnSceneLoaded;
//             }
//         }
//
//         #endregion
//
//         #region Public methods
//
//         public void AddScore(int points)
//         {
//             _score += points;
//             UpdateScoreText();
//         }
//
//         #endregion
//
//         #region Private methods
//
//         private void LinkScoreText()
//         {
//             ScoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
//             if (ScoreText == null)
//             {
//                 Debug.LogError(
//                     "ScoreText not found in the scene");
//             }
//
//             UpdateScoreText();
//         }
//
//         private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//         {
//             LinkScoreText();
//             ResetScore();
//         }
//
//         private void ResetScore()
//         {
//             _score = 0;
//             UpdateScoreText();
//         }
//
//         private void UpdateScoreText()
//         {
//             if (ScoreText != null)
//             {
//                 ScoreText.text = "Score: " + _score;
//             }
//         }
//
//         #endregion
//     }
// }