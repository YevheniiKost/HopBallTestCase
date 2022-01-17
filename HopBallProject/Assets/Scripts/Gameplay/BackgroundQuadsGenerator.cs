using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BackgroundQuadsGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _quadsParent;
        [SerializeField] private BackgroundQuad _quadPrefab;
        [Header("Parameters")]
        [SerializeField] private float _spawnHeight;
        [SerializeField] private float _disappearHeigh;
        [SerializeField] private Color _lightColor;
        [SerializeField] private Color _darkColor;
        [SerializeField, Range(0,1)] private float _minSpawnRate = 0;
        [SerializeField, Range(0,10)] private float _maxSpawntRate = 2;
        [SerializeField] private float _minQuadSize = 1.5f;
        [SerializeField] private float _maxQuadSize = 4;
        [SerializeField] private float _quadsXPosDelta = 2.5f;
       
        private bool _isSpawning;
        private float _nextSpawnTime;
        private Queue<BackgroundQuad> _quadsQueue = new Queue<BackgroundQuad>();
        private IMovableBackground _background;

        public void StartSpawn()
        {
            _isSpawning = true;
        }

        public void StopSpawn()
        {
            _isSpawning = false;
        }

        public void ReturnToPool(BackgroundQuad quad)
        {
            quad.gameObject.SetActive(false);
            quad.transform.SetParent(this.transform);
            quad.transform.localPosition = Vector3.zero;
            _quadsQueue.Enqueue(quad);
        }

        private void Start()
        {
            _background = ServiceLocator.SharedInstanse.Resolve<IMovableBackground>();  
        }

        private void Update()
        {
            if (_isSpawning)
            {
                _nextSpawnTime -= Time.deltaTime;
                if(_nextSpawnTime <= 0)
                {
                    SpawnQuad();
                    _nextSpawnTime = GetRandomSpawnTime();
                }
            }
        }

        private void SpawnQuad()
        {
            BackgroundQuad quad = GetQuadFroQueue();
            quad.InitQuad(GetRandomQuadSize()
                , new Vector3(UnityEngine.Random.Range(-_quadsXPosDelta, _quadsXPosDelta), _spawnHeight, 0)
                , GetRandomColor()
                , _disappearHeigh 
                , this);
            quad.transform.SetParent(_quadsParent, true);
        }

        private BackgroundQuad GetQuadFroQueue()
        {
            if (_quadsQueue.Count != 0)
            {
                return _quadsQueue.Dequeue();
            }
            else
            {
                return Instantiate(_quadPrefab, this.transform);
            }
        }

        private Vector2 GetRandomQuadSize()
        {
            return new Vector2(UnityEngine.Random.Range(_minQuadSize, _maxQuadSize), UnityEngine.Random.Range(_minQuadSize, _maxQuadSize));
        }

        private float GetRandomSpawnTime()
        {
            return UnityEngine.Random.Range(_minSpawnRate, _maxSpawntRate);
        }

        private Color GetRandomColor()
        {
            float randomNumber = UnityEngine.Random.Range(0f, 1f);
            return Color.Lerp(_lightColor, _darkColor, randomNumber);
        }
    }
}