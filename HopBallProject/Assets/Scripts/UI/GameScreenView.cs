using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class GameScreenView : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI _coinsTextMesh;
        [SerializeField] private TextMeshProUGUI _heightTextMesh;

        private string _heightText;

        public override ScreenType ScreenType => ScreenType.Game;

        protected override void OnShow()
        {
            ServiceLocator.SharedInstanse.Resolve<Gameplay.IMovableBackground>().OnHeightChange += OnHeighChangeHadnder;

            var playerWallet = ServiceLocator.SharedInstanse.Resolve<Core.Player.IPlayer>().Wallet;
            playerWallet.OnNumberOfCoinsChange += OnNumberOfCoinsHandler;
            SetCoinsText(playerWallet.GetCoins);
        }

        protected override void OnHide()
        {
            ServiceLocator.SharedInstanse.Resolve<Gameplay.IMovableBackground>().OnHeightChange -= OnHeighChangeHadnder;
            ServiceLocator.SharedInstanse.Resolve<Core.Player.IPlayer>().Wallet.OnNumberOfCoinsChange -= OnNumberOfCoinsHandler;
        }

        protected override void OnStart()
        {
            _heightText = _heightTextMesh.text;
            SetHeightText(0);
        }

        private void OnHeighChangeHadnder(float obj)
        {
            SetHeightText(obj);
        }
        private void OnNumberOfCoinsHandler(int obj)
        {
            SetCoinsText(obj);
        }
        private void SetHeightText(float height)
        {
            _heightTextMesh.text = String.Format(_heightText, height.ToString("00"));
        }
        private void SetCoinsText(int coins)
        {
            _coinsTextMesh.text = coins.ToString();
        }
    }
}