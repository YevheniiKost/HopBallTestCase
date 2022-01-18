using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameCoin : SpawItem
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<GameBall>())
            {
                EventAggregator.Post(this, new Events.OnGetCoinEvent());
                ReturnToPool();
            }
        }
    }
}