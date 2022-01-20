using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public class Player : IPlayer
    {
        public float HighScore { get; private set; }

        public IWallet Wallet { get; private set; }

        public IVault Vault { get; private set; }

        public Player(float currentHighScore, IWallet wallet, IVault vault)
        {
            HighScore = currentHighScore;
            Wallet = wallet;
            Vault = vault;
        }

        public static IPlayer Default()
        {
            var wallet = Core.Player.Wallet.Default();
            var vault = Core.Player.PlayerVault.Default();
            return new Player(0, wallet, vault);
        }

        public void GetNewHighScore(float value)
        {
            HighScore = value;
        }
    }

    public interface IPlayer
    {
        float HighScore { get; }
        void GetNewHighScore(float value);
        IWallet Wallet { get; }
        IVault Vault { get; }
    }
}