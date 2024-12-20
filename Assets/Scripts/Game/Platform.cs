﻿using System;
using Arcanoid.Services;
using UnityEngine;

namespace Arcanoid.Game
{
    public class Platform : MonoBehaviour
    {
        #region Events

        public static event Action<Platform> OnCreated;
        public static event Action<Platform> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            OnCreated?.Invoke(this);
        }

        private void Update()
        {
            if (PauseService.Instance.IsPaused)
            {
                return;
            }

            if (GameService.Instance.IsAutoPlay)
            {
                MoveWithBall();
            }
            else
            {
                MoveWithMouse();
            }
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        #endregion

        #region Private methods

        private void MoveWithBall()
        {
            Ball ball = LevelService.Instance.Ball;
            if (ball == null)
            {
                return;
            }

            SetXPosition(ball.transform.position.x);
        }

        private void MoveWithMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            SetXPosition(worldPosition.x);
        }

        private void SetXPosition(float x)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = x;
            transform.position = currentPosition;
        }

        #endregion
    }
}