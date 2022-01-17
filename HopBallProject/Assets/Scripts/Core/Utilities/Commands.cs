using Core.UI;
using Core.Utilities;
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
        public PlayCommand()
        {
            // init play game logic
        }
        public void Execute()
        {
            //play game logic
            Debug.Log("Play game");
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
            _uiManager.OpenScreen(ScreenType.Main);
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
    }
}

