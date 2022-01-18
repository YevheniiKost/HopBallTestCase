using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public interface IWallet
    {
        int GetCoins { get; }
        int GetGoldCoins { get; }
        void AddCoins(int amount);
        bool TryToSpendGoldCoins(int amount);
        event Action<int> OnNumberOfCoinsChange;
    }

    public class Wallet : IWallet
    {
        private const int CoinsToGoldMultiplier = 10;

        private int _coins;
        private int _goldCoins;

      
        public Wallet(int coins, int goldCoins)
        {
            _coins = coins;
            _goldCoins = goldCoins;
        }

        public int GetCoins => _coins;
        public int GetGoldCoins => _goldCoins;

        public event Action<int> OnNumberOfCoinsChange;

        public static IWallet Default()
        {
            return new Wallet(0, 0);
        }

        public void AddCoins(int amount)
        {
            _coins += amount;
            _goldCoins += amount * CoinsToGoldMultiplier;
            OnNumberOfCoinsChange?.Invoke(_coins);
        }

        public bool TryToSpendGoldCoins(int amount)
        {
            if(amount <= _goldCoins)
            {
                _goldCoins -= amount;
                return true;
            } else
            {
                return false;
            }
        }
    }
}