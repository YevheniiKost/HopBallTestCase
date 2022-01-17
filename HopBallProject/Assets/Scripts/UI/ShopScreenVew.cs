using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class ShopScreenVew : BaseScreen
    {
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private BallShopItemView _shopItemPrefab;
        [SerializeField] private Button _backButton;

        [Header("Temp")]
        [SerializeField] private List<BallShopItemDescriptor> _ballDescriptors;

        public override ScreenType ScreenType => ScreenType.Shop;

        private Commands.IGameCommand _exitShopCommand;
        private Commands.ITryByeBallCommand _tryByeBallCommand;

        protected override void OnAwake()
        {
            _backButton.onClick.AddListener(OnBackButtonClickHandler);
        }

        protected override void OnStart()
        {
            PrepareCommands();
            CreateShop(_ballDescriptors);
        }

        private void CreateShop(List<BallShopItemDescriptor> ballDescriptors)
        {
            foreach (var descriptor in ballDescriptors)
            {
                var prefab = Instantiate(_shopItemPrefab, _itemsContainer);
                prefab.Init(descriptor);
                prefab.OnItemClicked += OnShopItemClick;
            }
        }

        private void PrepareCommands()
        {
            _exitShopCommand = Factory.Command.CreateExitShopCommand();
            _tryByeBallCommand = Factory.Command.CreateTryByeBallCommand();
        }

        private void OnShopItemClick(BallShopItemDescriptor descriptor)
        {
            _tryByeBallCommand.Execute(descriptor);
        }

        private void OnBackButtonClickHandler()
        {
            _exitShopCommand.Execute();
        }
    }

    [System.Serializable]
    public class BallShopItemDescriptor
    {
        public Color Color;
        public string Name;
        public int Price;
    }
}