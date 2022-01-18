using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public interface IVault
    {
        List<BallShopItem> PurchasedBalls { get; }
        void PurchaseBall(BallShopItem ball);
    }

    public class PlayerVault : IVault
    {
        public List<BallShopItem> PurchasedBalls { get; private set; }


        public PlayerVault(List<BallShopItem> ballsList)
        {
            PurchasedBalls = ballsList;
        }

        public static IVault Default()
        {
            return new PlayerVault(new List<BallShopItem>());
        }

        public void PurchaseBall(BallShopItem ball)
        {
            PurchasedBalls.Add(ball);
        }
    }
}