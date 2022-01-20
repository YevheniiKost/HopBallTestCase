using Core.Player;
using Core.Utilities;
using Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class HighscoreSpawner : ItemGenerator
    {
        private IPlayer _player;
        private IGameplayController _gameController;
        private bool _isHighScoreShowed = false;

        private void Awake()
        {
            EventAggregator.Subscribe<Events.OnGameStarted>(OnGameStartedHandler);
        }

        protected override void Start()
        {
            base.Start();
            _player = ServiceLocator.SharedInstanse.Resolve<IPlayer>();
            _gameController = ServiceLocator.SharedInstanse.Resolve<IGameplayController>();
            _background.OnHeightChange += OnHeighChangeHandler;
        }

        private void OnDestroy()
        {
            _background.OnHeightChange -= OnHeighChangeHandler;
            EventAggregator.Unsubscribe<Events.OnGameStarted>(OnGameStartedHandler);
        }

        private void OnGameStartedHandler(object arg1, OnGameStarted arg2)
        {
            _isHighScoreShowed = false;
        }

        private void OnHeighChangeHandler(float currentHeight)
        {
            if (_isHighScoreShowed)
                return;

            if(_player.HighScore != 0 &&(currentHeight + _spawnHeight) >= _player.HighScore)
            {
                SpawnItem();
                _isHighScoreShowed = true;
            }
        }
    }
}