using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public interface IShopScreenView : IView
    {
        void InitShop(List<BallItemDescriptor> items);
    }

    public class ShopScreenVew : BaseScreen, IShopScreenView
    {
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private BallShopItemView _shopItemPrefab;
        [SerializeField] private Button _backButton;

        [Header("Temp")]
        [SerializeField] private List<BallItemDescriptor> _defaultItems;

        public override ScreenType ScreenType => ScreenType.Shop;

        private Commands.IGameCommand _exitShopCommand;
        private Commands.ITryByeBallCommand _tryByeBallCommand;

        public void InitShop(List<BallItemDescriptor> items)
        {
            if (items != null)
            {
                CreateShop(items);
            }
            else
            {
                CreateShop(_defaultItems);
            }
        }

        protected override void OnAwake()
        {
            _backButton.onClick.AddListener(OnBackButtonClickHandler);
        }

        protected override void OnStart()
        {
            PrepareCommands();
        }

        private void CreateShop(List<BallItemDescriptor> ballDescriptors)
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

        private void OnShopItemClick(BallItemDescriptor descriptor)
        {
            _tryByeBallCommand.Execute(descriptor);
        }

        private void OnBackButtonClickHandler()
        {
            _exitShopCommand.Execute();
        }

        
    }

    [System.Serializable]
    public class BallItemDescriptor
    {
        public Color Color;
        public string Name;
        public int Price;
        public float Weight;
        public float Radius;
    }
}