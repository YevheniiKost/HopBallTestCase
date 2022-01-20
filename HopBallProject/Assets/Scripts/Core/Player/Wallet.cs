using Core.Backend;
using Core.Utilities;
using Events;
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
            EventAggregator.Subscribe<Events.OnGameEnded>(OnGameEndedHandler);
            EventAggregator.Subscribe<Events.OnGameStarted>(OnGameStartedHandler);
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

        private void OnGameStartedHandler(object arg1, OnGameStarted arg2)
        {
            var playfabManager = ServiceLocator.SharedInstanse.Resolve<IPlayfabManager>();
            var currencyData = playfabManager.PlayerCurrencyData;
            _coins = currencyData.Coins;
            _goldCoins = currencyData.GoldCoins;
            OnNumberOfCoinsChange?.Invoke(_coins);
        }

        private void OnGameEndedHandler(object arg1, OnGameEnded data)
        {
            var playfabManager = ServiceLocator.SharedInstanse.Resolve<IPlayfabManager>();
            playfabManager.SetCurrencyData(new PlayerCurrencyData { Coins = _coins, GoldCoins = _goldCoins });
        }
    }

    public class PlayerCurrencyData
    {
        public int Coins;
        public int GoldCoins;
    }

    public enum PlayerCurrencyType
    {
        Coins,
        GoldCoins
    }
}