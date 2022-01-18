using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EndGameCollider : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<GameBall>())
            {
                EventAggregator.Post(this, new Events.OnBallFallEvent { });
            }
        }
    }
}