using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BackgroundQuad : MonoBehaviour
    {
        private const int MinLayer = 1;
        private const int MaxLayer = 5;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _minYPosition;
        private BackgroundQuadsGenerator _generator;

        public void InitQuad(Vector2 size, Vector3 position, Color color, float minYPos, BackgroundQuadsGenerator generator)
        {
            gameObject.SetActive(true);
            _spriteRenderer.sortingOrder = UnityEngine.Random.Range(MinLayer, MaxLayer);
            _spriteRenderer.size = size;
            transform.position = position;
            _spriteRenderer.color = color;
            _minYPosition = minYPos;
            _generator = generator;
        }

        private void Update()
        {
            if (OnMinimalHeight())
            {
                ReturnToPool();
            }
        }

        private bool OnMinimalHeight()
        {
            return transform.position.y <= _minYPosition;
        }

        private void ReturnToPool()
        {
            _generator.ReturnToPool(this);
        }
    }
}