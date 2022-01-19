using Core.Player;
using Core.UI;
using Core.Utilities;
using Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public interface IGameplayController
    {
        void PrepareToGame();
        void StartGame();
        void StopGame();
        void ResetGame();
        event Action OnGameStarted;
        event Action OnGameEnded;
    }
    public class GameplayController : MonoBehaviour, IGameplayController
    {
        [SerializeField] private JoystickController _leftJoystick;
        [SerializeField] private JoystickController _rightJoystick;
        [SerializeField] private GameBall _ball;

        private Vector3 _ballInitialPosition;
        private Vector3 _leftJoystickInitialPosition;
        private Vector3 _righJoystickInitialPosition;

        private IPlayer _player;
        private IMovableBackground _background;

        public event Action OnGameStarted;
        public event Action OnGameEnded;

        public void PrepareToGame()
        {
            ActivateJoystics(true);
            _ball.gameObject.SetActive(true);
        }

        public void StartGame()
        {
            _ball.ActivateBall();
            GetGameElementsPositions();
            _leftJoystick.UnblockInput();
            _rightJoystick.UnblockInput();
            OnGameStarted?.Invoke();
        }

        public void StopGame()
        {
            ActivateJoystics(false);
            _ball.DisactivateBall();
            _ball.gameObject.SetActive(false);
            CheckHighScore();
            ActivateEndGameUI();
            ResetGame();
            OnGameEnded?.Invoke();
        }

        public void ResetGame()
        {
            _ball.transform.position = _ballInitialPosition;
            _leftJoystick.transform.position = _leftJoystickInitialPosition;
            _rightJoystick.transform.position = _righJoystickInitialPosition;

            _background.Reset();
        }

        private void Awake()
        {
            ServiceLocator.SharedInstanse.Register<IGameplayController>(this);
            EventAggregator.Subscribe<Events.OnBallFallEvent>(OnBallFallHandler);
            EventAggregator.Subscribe<Events.OnGetCoinEvent>(OnGetCoinHandler);
        }

        private void Start()
        {
            ActivateJoystics(false);
            _player = ServiceLocator.SharedInstanse.Resolve<IPlayer>();
            _background = ServiceLocator.SharedInstanse.Resolve<IMovableBackground>();
        }

        private void OnDestroy()
        {
            ServiceLocator.SharedInstanse.Unregister<IGameplayController>();
            EventAggregator.Unsubscribe<Events.OnBallFallEvent>(OnBallFallHandler);
            EventAggregator.Unsubscribe<Events.OnGetCoinEvent>(OnGetCoinHandler);
        }

        private void OnBallFallHandler(object arg1, OnBallFallEvent arg2)
        {
            StopGame();
        }

        private void ActivateJoystics(bool isActivate)
        {
            _leftJoystick.gameObject.SetActive(isActivate);
            _rightJoystick.gameObject.SetActive(isActivate);

            if (!isActivate)
            {
                _leftJoystick.BlockInput();
                _rightJoystick.BlockInput();
            }
        }
        private void ActivateEndGameUI()
        {
            var background = ServiceLocator.SharedInstanse.Resolve<IMovableBackground>();
            var uiManaget = ServiceLocator.SharedInstanse.Resolve<IUIManager>();
            var winWindow = (IWinScreenView)uiManaget.GetWindow(ScreenType.Win);
            uiManaget.OpenScreen(ScreenType.Win);
            winWindow.SetScore(Mathf.RoundToInt(background.CurrentHeight));
        }
        private void GetGameElementsPositions()
        {
            _ballInitialPosition = _ball.transform.position;
            _leftJoystickInitialPosition = _leftJoystick.transform.position;
            _righJoystickInitialPosition = _rightJoystick.transform.position;
        }

        private void OnGetCoinHandler(object arg1, OnGetCoinEvent arg2)
        {
            _player.Wallet.AddCoins(1);
        }

        private void CheckHighScore()
        {
            if(_background.CurrentHeight > _player.HighScore)
            {
                _player.GetNewHighScroe(_background.CurrentHeight);
            }
        }

    }
}