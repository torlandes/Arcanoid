using System;
using System.Collections.Generic;
using Arcanoid.Game;
using Arcanoid.Utility;

namespace Arcanoid.Services
{
    public class LevelService : SingletonMonoBehaviour<LevelService>
    {
        #region Variables

        private readonly List<Ball> _balls = new();
        private readonly List<Block> _blocks = new();

        #endregion

        #region Events

        public event Action OnAllBlocksDestroyed;

        #endregion

        #region Properties

        public Ball Ball { get; private set; }
        public Platform Platform { get; private set; }
        public List<Ball> Balls => _balls;
        public IReadOnlyList<Block> Blocks => _blocks;

        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();

            Block.OnCreated += BlockCreatedCallback;
            Block.OnDestroyed += BlockDestroyedCallback;

            Ball.OnCreated += BallCreatedCallback;
            Ball.OnDestroyed += BallDestroyedCallback;
        }

        private void OnDestroy()
        {
            Block.OnCreated -= BlockCreatedCallback;
            Block.OnDestroyed -= BlockDestroyedCallback;

            Ball.OnCreated -= BallCreatedCallback;
            Ball.OnDestroyed -= BallDestroyedCallback;
        }

        #endregion

        #region Public methods

        public Ball GetFirstBall()
        {
            if (_balls.Count == 0)
            {
                return null;
            }

            return _balls[0];
        }

        public bool IsLastBall()
        {
            return _balls.Count < 2;
        }

        public void ResetBalls()
        {
            foreach (Ball ball in _balls)
            {
                ball.ResetBall();
                if (GameService.Instance.IsAutoPlay)
                {
                    ball.ForceStart();
                }
            }
        }
        
        #endregion

        #region Private methods

        private void BallCreatedCallback(Ball ball)
        {
            Ball = ball;
            _balls.Add(ball);
        }

        private void BallDestroyedCallback(Ball ball)
        {
            Ball = null;
            _balls.Remove(ball);
            if (_balls.Count == 0)
            {
                GameService.Instance.CheckGameEnd();
            }
        }

        private void BlockCreatedCallback(Block block)
        {
            _blocks.Add(block);
        }

        private void BlockDestroyedCallback(Block block)
        {
            _blocks.Remove(block);

            if (_blocks.Count == 0)
            {
                OnAllBlocksDestroyed?.Invoke();
            }
        }

        #endregion
    }
}