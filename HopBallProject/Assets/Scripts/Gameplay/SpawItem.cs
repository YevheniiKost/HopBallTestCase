using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class SpawItem : MonoBehaviour
    {
        private float _minYPosition;
        private ItemGenerator _generator;

        public virtual void InitItem(Vector3 position, float minYPosition, ItemGenerator generator)
        {
            gameObject.SetActive(true);
            transform.position = position;
            _minYPosition = minYPosition;
            _generator = generator;
        }
        protected void ReturnToPool()
        {
            _generator.ReturnToPool(this);
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
    }
}