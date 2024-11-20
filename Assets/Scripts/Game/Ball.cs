using System;
using Arcanoid.Services;
using Arcanoid.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arcanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Vector2 _startDirection;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _yOffsetFromPlatform = 1;

        [Header("Audio")]
        [SerializeField] private AudioClip _hitAudioClip;

        [Header("Direction")]
        [SerializeField] private float _directionMin = -90;
        [SerializeField] private float _directionMax = 90;
        [SerializeField] private int _segments = 10;

        private bool _isStarted;
        private Platform _platform;

        #endregion

        #region Events

        public static event Action<Ball> OnCreated;
        public static event Action<Ball> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();

            OnCreated?.Invoke(this);

            if (GameService.Instance.IsAutoPlay)
            {
                StartFlying();
            }
        }

        private void Update()
        {
            if (_isStarted)
            {
                return;
            }

            MoveWithPlatform();

            if (Input.GetMouseButtonDown(0))
            {
                StartFlying();
            }
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            AudioService.Instance.PlaySfx(_hitAudioClip);
        }

        private void OnDrawGizmos()
        {
            if (!_isStarted)
            {
                Gizmos.color = Color.green;
                GizmosUtility.DrawArc2D(transform.position, Vector2.up, _directionMin, _directionMax, _speed,
                    _segments);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_rb.velocity);
            }
        }

        #endregion

        #region Public methods

        public void ChangeSpeed(float speedChange)
        {
            _speed += speedChange;
            _rb.velocity = _rb.velocity.normalized * _speed;
        }

        public void ForceStart()
        {
            _isStarted = true;
        }
        public Rigidbody2D GetRigidBody()
        {
            return _rb;
        }

        public Vector2 GetRandomStartDirection()
        {
            float minAngleDeg = -75f;
            float maxAngleDeg = 75f;
            float minAngleRad = minAngleDeg * Mathf.Deg2Rad;
            float maxAngleRad = maxAngleDeg * Mathf.Deg2Rad;
            float randomAngle = Random.Range(minAngleRad, maxAngleRad);
            Vector2 direction = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle)).normalized;

            return direction;
        }

        public void ResetBall()
        {
            _isStarted = false;
            _rb.velocity = Vector2.zero;
        }

        #endregion

        #region Private methods

        private void MoveWithPlatform()
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = _platform.transform.position.x;
            currentPosition.y = _platform.transform.position.y + _yOffsetFromPlatform;
            transform.position = currentPosition;
        }

        private void StartFlying()
        {
            _isStarted = true;
            Vector2 randomDirection = GetRandomStartDirection();
            _rb.velocity = randomDirection * _speed;
        }

        #endregion
    }
}