using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] protected Transform _itemParent;
        [SerializeField] private SpawItem _itemPrefab;
        [Header("Parameters")]
        [SerializeField] protected float _spawnHeight;
        [SerializeField] protected float _disappearHeigh;
        [SerializeField, Range(0,1)] private float _minSpawnRate = 0;
        [SerializeField, Range(0,10)] private float _maxSpawntRate = 2;
        [SerializeField] protected float _itemXPosDelta = 2.5f;

        protected IMovableBackground _background;
        private bool _isSpawning;
        private float _nextSpawnTime;
        private Queue<SpawItem> _quadsQueue = new Queue<SpawItem>();

        public void StartSpawn()
        {
            _isSpawning = true;
        }

        public void StopSpawn()
        {
            _isSpawning = false;
        }

        public void SpawnItemImmediately()
        {
            SpawnItem();
        }

        public void ReturnToPool(SpawItem item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(this.transform);
            item.transform.localPosition = Vector3.zero;
            _quadsQueue.Enqueue(item);
        }

        protected virtual void Start()
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
                    SpawnItem();
                    _nextSpawnTime = GetRandomSpawnTime();
                }
            }
        }

        protected virtual void SpawnItem()
        {
            SpawItem item = GetItemFromQueue();
            item.InitItem(new Vector3(UnityEngine.Random.Range(-_itemXPosDelta, _itemXPosDelta), _spawnHeight, 0)
                , _disappearHeigh
                , this);
            item.transform.SetParent(_itemParent, true);
        }

        protected SpawItem GetItemFromQueue()
        {
            if (_quadsQueue.Count != 0)
            {
                return _quadsQueue.Dequeue();
            }
            else
            {
                return Instantiate(_itemPrefab, this.transform);
            }
        }

        private float GetRandomSpawnTime()
        {
            return UnityEngine.Random.Range(_minSpawnRate, _maxSpawntRate);
        }
    }
}