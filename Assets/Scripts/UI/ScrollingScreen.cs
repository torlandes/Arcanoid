using UnityEngine;
using UnityEngine.UI;

namespace Arcanoid.UI
{
    public class ScrollingScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private RawImage _img;
        [SerializeField] private float _x, _y;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
           _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
        }

        #endregion
    }
}