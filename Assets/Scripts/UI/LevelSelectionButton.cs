using Arcanoid.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arcanoid.UI
{
    public class LevelSelectionButton : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private string _levelName;
        

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _button.onClick.AddListener(SelectLevelClickedCallback);
        }

        #endregion

        #region Private methods

        private void SelectLevelClickedCallback()
        {
            SceneLoaderService.Instance.ChooseLevel(levelName:_levelName);
        }

        #endregion
    }
}