using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public interface IWinScreenView : IView
    {
        void SetScore(int score);
    }
    public class WinScreenView : BaseScreen, IWinScreenView
    {
        [SerializeField] private TextMeshProUGUI _scoreTextMesh;
        [SerializeField] private Button _retryButton;
        public override ScreenType ScreenType => ScreenType.Win;

        private Commands.IGameCommand _exitWinScreenCommand;

        public void SetScore(int score)
        {
            _scoreTextMesh.text = score.ToString();
        }

        protected override void OnAwake()
        {
            _retryButton.onClick.AddListener(OnRetryButtonClickHandler);
        }

        protected override void OnStart()
        {
            _exitWinScreenCommand = Factory.Command.CreateExitWinScreenCommand();
            base.OnStart();
        }

        private void OnRetryButtonClickHandler()
        {
            _exitWinScreenCommand.Execute();
        }
    }
}