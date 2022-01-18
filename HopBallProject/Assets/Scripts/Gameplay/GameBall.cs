using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameBall : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _initialForce = 10f;

        private void Start()
        {
            DisactivateBall();
            gameObject.SetActive(false);
        }

        public void ActivateBall()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.AddForce(Vector2.left * _initialForce, ForceMode2D.Impulse);
        }

        public void DisactivateBall()
        {
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
}