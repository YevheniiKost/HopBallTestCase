using Core.UI;
using Core.Utilities;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public interface IGameCommand 
    {
        void Execute();
    }

    public interface ITryByeBallCommand
    {
        void Execute(BallShopItemDescriptor descriptor);
    }

    public class LoginCommand : IGameCommand
    {
        private IUIManager _uiManager;
        public LoginCommand()
        {
            _uiManager = ServiceLocator.SharedInstanse.Resolve<IUIManager>();
        }
        public void Execute()
        {
            Debug.Log("Player log in");
            _uiManager.CloseScreen(ScreenType.Login);
            _uiManager.OpenScreen(ScreenType.Main);
        }
    }

    public class PlayCommand : IGameCommand
    {
        private IUIManager _uiManager;
        private IGameplayController _gameplayController;

        public PlayCommand()
        {
            _uiManager = ServiceLocator.SharedInstanse.Resolve<IUIManager>();
            _gameplayController = ServiceLocator.SharedInstanse.Resolve<IGameplayController>();
        }
        public void Execute()
        {
            _uiManager.CloseScreen(ScreenType.Main);
            _uiManager.OpenScreen(ScreenType.PrepareToGame);
            _gameplayController.PrepareToGame();
        }
    }

    public class ShopCommand : IGameCommand
    {
        private IUIManager _uiManager;
        public ShopCommand()
        {
            _uiManager = ServiceLocator.SharedInstanse.Resolve<IUIManager>();
        }

        public void Execute()
        {
            _uiManager.CloseScreen(ScreenType.Main);
            _uiManager.OpenScreen(ScreenType.Shop);
        }
    }

    public class TryByeBallCommand : ITryByeBallCommand
    {
        public TryByeBallCommand()
        {

        }
        public void Execute(BallShopItemDescriptor descriptor)
        {
            Debug.Log($"Try to bye {descriptor.Name}");
        }
    }

    public class ExitShopCommand : IGameCommand
    {
        private IUIManager _uiManager;
        public ExitShopCommand()
        {
            _uiManager = ServiceLocator.SharedInstanse.Resolve<IUIManager>();
        }
        public void Execute()
        {
            _uiManager.CloseScreen(ScreenType.Shop);
            _uiManager.OpenScreen(ScreenType.Main);
        }
    }

    public class ExitWinSreenCommand : IGameCommand
    {
        private IUIManager _uiManager;
        public ExitWinSreenCommand()
        {
            _uiManager = ServiceLocator.SharedInstanse.Resolve<IUIManager>();
        }
        public void Execute()
        {
            _uiManager.CloseScreen(ScreenType.Win);
            _uiManager.CloseScreen(ScreenType.Game);
            _uiManager.OpenScreen(ScreenType.Main);
        }
    }

    public class OnPrepareGameClickCommand : IGameCommand
    {
        private IGameplayController _gameplayController;
        private IUIManager _uIManager;
        public OnPrepareGameClickCommand()
        {
            _gameplayController = ServiceLocator.SharedInstanse.Resolve<IGameplayController>();
            _uIManager = ServiceLocator.SharedInstanse.Resolve<IUIManager>();
        }
        public void Execute()
        {
            _uIManager.CloseScreen(ScreenType.PrepareToGame);
            _uIManager.OpenScreen(ScreenType.Game);
            _gameplayController.StartGame();
        }
    }
}

public static partial class Factory
{
    public static class Command
    {
        public static Commands.IGameCommand CreateLoginCommand()
        {
            return new Commands.LoginCommand();
        }
        public static Commands.IGameCommand CreatePlayCommand()
        {
            return new Commands.PlayCommand();
        }
        public static Commands.IGameCommand CreateShopCommand()
        {
            return new Commands.ShopCommand();
        }
        public static Commands.ITryByeBallCommand CreateTryByeBallCommand()
        {
            return new Commands.TryByeBallCommand();
        }
        public static Commands.IGameCommand CreateExitShopCommand()
        {
            return new Commands.ExitShopCommand();
        }
        public static Commands.IGameCommand CreateExitWinScreenCommand()
        {
            return new Commands.ExitWinSreenCommand();
        }
        public static Commands.IGameCommand CreatePrepareGameClickCommand()
        {
            return new Commands.OnPrepareGameClickCommand();
        }
    }
}

