using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BackgroundQuad : SpawItem
    {
        private const int MinLayer = 1;
        private const int MaxLayer = 5;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void InitItem(Vector2 size, Vector3 position, Color color, float minYPos, ItemGenerator generator) 
        {
            base.InitItem(position, minYPos, generator);
            _spriteRenderer.sortingOrder = UnityEngine.Random.Range(MinLayer, MaxLayer);
            _spriteRenderer.size = size;
            _spriteRenderer.color = color;
        }
    }
}