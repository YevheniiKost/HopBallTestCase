using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public interface IMovableBackground
    {
        void MoveDown(float speedMultiplier);
        void StopMovement();
        float CurrentHeight { get; }
    }

    public class MovableBackground : MonoBehaviour, IMovableBackground
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private BackgroundQuadsGenerator _generator;

        private bool _isMovingDown;
        private float _currentSpeedMultiplier;

        public float CurrentHeight => -transform.position.y;

        public void MoveDown(float speedMultiplier)
        {
            _isMovingDown = true;
            _currentSpeedMultiplier = speedMultiplier;
            _generator.StartSpawn();
        }

        public void StopMovement()
        {
            _isMovingDown = false;
            _generator.StopSpawn();
        }

        private void Awake()
        {
            ServiceLocator.SharedInstanse.Register<IMovableBackground>(this);
        }

        private void Update()
        {
            if (_isMovingDown)
                MoveDown();
        }

        private void OnDestroy()
        {
            ServiceLocator.SharedInstanse.Unregister<IMovableBackground>();
        }

        private void MoveDown()
        {
            transform.position += Vector3.down * _movementSpeed * _currentSpeedMultiplier * Time.deltaTime;
        }

        [ContextMenu("Move")]
        private void MoveTest()
        {
            MoveDown(1);
        }

        [ContextMenu("Stop")]
        private void StopMoveTest()
        {
            StopMovement();
        }
    }
}