using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class LoginScreenView : BaseScreen
    {
        [SerializeField] private Button _loginButton;
        public override ScreenType ScreenType => ScreenType.Login;

        private Commands.IGameCommand _loginCommand;

        protected override void OnAwake()
        {
            _loginCommand = Factory.Command.CreateLoginCommand();
            _loginButton.onClick.AddListener(OnLoginButtonClickHandler);
        }

        private void OnLoginButtonClickHandler()
        {
            _loginCommand.Execute();
        }
    }
}
