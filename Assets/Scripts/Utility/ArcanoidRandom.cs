using UnityEngine;

namespace Arcanoid.Utility
{
    public static class ArcanoidRandom
    {
        #region Public methods

        public static Vector2 GetRandomVector2()
        {
            float fi = Random.Range(0f, 2 * Mathf.PI);
            return new Vector3(Mathf.Cos(fi), Mathf.Sin(fi));
        }

        public static Vector3 GetRandomVector3()
        {
            float fi = Random.Range(0f, 2 * Mathf.PI);
            return new Vector3(Mathf.Cos(fi), Mathf.Sin(fi), 0);
        }

        #endregion
    }
}