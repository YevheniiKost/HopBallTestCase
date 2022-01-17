using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BackroundSpeedChangeCollider : MonoBehaviour
    {
        [SerializeField] private float _maxBallHeigh;
        private IMovableBackground _movableBackground;

        private void Start()
        {
            _movableBackground = ServiceLocator.SharedInstanse.Resolve<IMovableBackground>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent<GameBall>(out var ball))
            {
                _movableBackground.MoveDown(GetSpeedMultiplier(ball.transform.position.y));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<GameBall>())
            {
                _movableBackground.StopMovement();
            }
        }

        private float GetSpeedMultiplier(float ballYPos)
        {
            float heighDifference = ballYPos - transform.position.y;
            heighDifference = Mathf.Clamp(heighDifference, 0, _maxBallHeigh);
            return Mathf.InverseLerp(0, 2, heighDifference);
        }
    }
}
