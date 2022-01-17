using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class MainScreenView : BaseScreen
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _shopButton;

        public override ScreenType ScreenType => ScreenType.Main;

        private Commands.IGameCommand _playCommand;
        private Commands.IGameCommand _shopCommand;

        protected override void OnAwake()
        {
            RegisterButtons();
        }

        protected override void OnStart()
        {
            CreateCommands();
        }

        private void RegisterButtons()
        {
            _playButton.onClick.AddListener(OnPlayButtonClickHandler);
            _shopButton.onClick.AddListener(OnShopButtonClickHandler);
        }

        private void CreateCommands()
        {
            _playCommand = Factory.Command.CreatePlayCommand();
            _shopCommand = Factory.Command.CreateShopCommand();
        }

        private void OnPlayButtonClickHandler()
        {
            _playCommand.Execute();
        }

        private void OnShopButtonClickHandler()
        {
            _shopCommand.Execute();
        }
    }
}