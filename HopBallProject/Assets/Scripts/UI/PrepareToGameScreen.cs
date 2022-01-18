using Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class PrepareToGameScreen : BaseScreen
    {
        [SerializeField] private Button _prepareGameScreenButton;
        public override ScreenType ScreenType => ScreenType.PrepareToGame;

        private IGameCommand _prepareGameClickCommand;

        protected override void OnAwake()
        {
            _prepareGameScreenButton.onClick.AddListener(OnPreapreButtonClickHandler);
        }

        protected override void OnStart()
        {
            _prepareGameClickCommand = Factory.Command.CreatePrepareGameClickCommand();
        }

        private void OnPreapreButtonClickHandler()
        {
            _prepareGameClickCommand.Execute();
        }
    }
}