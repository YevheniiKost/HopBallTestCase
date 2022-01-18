using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{

    public class BackgroundQuadGenerator : ItemGenerator
    {
        [SerializeField] private Color _lightColor;
        [SerializeField] private Color _darkColor;
        [SerializeField] private float _minQuadSize = 1.5f;
        [SerializeField] private float _maxQuadSize = 4;

        protected override void SpawnItem()
        {
            BackgroundQuad quad = (BackgroundQuad)GetItemFromQueue();
            quad.InitItem(GetRandomQuadSize()
                , new Vector3(UnityEngine.Random.Range(-_itemXPosDelta, _itemXPosDelta), _spawnHeight, 0)
                , GetRandomColor()
                , _disappearHeigh
                , this);
            quad.transform.SetParent(_itemParent, true);
        }
        private Vector2 GetRandomQuadSize()
        {
            return new Vector2(UnityEngine.Random.Range(_minQuadSize, _maxQuadSize), UnityEngine.Random.Range(_minQuadSize, _maxQuadSize));
        }
        private Color GetRandomColor()
        {
            float randomNumber = UnityEngine.Random.Range(0f, 1f);
            return Color.Lerp(_lightColor, _darkColor, randomNumber);
        }
    }
}