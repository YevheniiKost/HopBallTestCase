using Core.Player;
using Core.Utilities;
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

        protected override void Start()
        {
            base.Start();
            _player = ServiceLocator.SharedInstanse.Resolve<IPlayer>();
            _background.OnHeightChange += OnHeighChangeHandler;
            _gameController.OnGameStarted += OnGameStartedHandler;
        }

        private void OnDestroy()
        {
            _background.OnHeightChange -= OnHeighChangeHandler;
            _gameController.OnGameStarted -= OnGameStartedHandler;
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

        private void OnGameStartedHandler()
        {
            _isHighScoreShowed = false;
        }
    }
}