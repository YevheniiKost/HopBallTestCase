using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class JoystickController : MonoBehaviour, IJoysticController
    {
        [SerializeField] private float _maxVerticalYPosition;
        [SerializeField] private float _minVertivalYPosition;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _lerpSpeed;
        [SerializeField] private Rigidbody2D _rigidbody;

        private float _xPos;
        private float _forceReduceFactor = .5f;

        public Collider2D MyCollider => _collider;

        private float _currentVelocity;

        private float _previousYPos = 0;

        public void Move(Vector2 direction)
        {
            direction.y = Mathf.Clamp(direction.y, _minVertivalYPosition, _maxVerticalYPosition);
            direction.x = _xPos;
            transform.position = Vector2.Lerp(transform.position, direction, _lerpSpeed);
        }

        private void Awake()
        {
            _xPos = transform.position.x;
        }

        private void Update()
        {
            _currentVelocity = GetVelocity();
            _previousYPos = transform.position.y;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<GameBall>())
            {
                var rb = collision.collider.GetComponent<Rigidbody2D>();
                rb.AddForce(collision.contacts[0].normal * _currentVelocity * _forceReduceFactor, ForceMode2D.Impulse);
            }   
        }

        private float GetVelocity()
        {
            return Mathf.Abs(_previousYPos - transform.position.y) / Time.deltaTime;
        }
    }

    public interface IJoysticController
    {
        void Move(Vector2 direction);
        Collider2D MyCollider { get; }
    }
}
