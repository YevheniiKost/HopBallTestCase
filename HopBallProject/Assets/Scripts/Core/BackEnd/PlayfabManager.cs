using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Core.Utilities;
using System;
using Core.Player;
using System.Linq;
using Newtonsoft.Json;

namespace Core.Backend
{
    public interface IPlayfabManager
    {
        void Login();
        void SendLeaderboard(int score);
        void SetCurrencyData(PlayerCurrencyData currencyData);
        void GetCurrencyData();
        void GetShopItemsData();
        PlayerCurrencyData PlayerCurrencyData { get; }
    }
    public class PlayfabManager : MonoBehaviour, IPlayfabManager
    {
        public PlayerCurrencyData PlayerCurrencyData
        {
            get
            {
                if(_tempPlayerCurrencyData != null)
                {
                    return _tempPlayerCurrencyData;
                } else
                {
                    return new PlayerCurrencyData { Coins = 0, GoldCoins = 0 };
                }
            }
        }

        private PlayerCurrencyData _tempPlayerCurrencyData;

        public void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
        }

        public void SendLeaderboard(int score)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "HighScore",
                        Value = score
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardRequesError);
        }

        public void SetCurrencyData(PlayerCurrencyData currencyData)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    {PlayerCurrencyType.Coins.ToString(), currencyData.Coins.ToString() },
                    {PlayerCurrencyType.GoldCoins.ToString(), currencyData.GoldCoins.ToString() }
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnDataSent, OnDataSendError);
        }

        public void GetCurrencyData()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataResieved, OnDataRecievedError);
        }

        public void GetShopItemsData()
        {
            var request = new GetCatalogItemsRequest
            {
                CatalogVersion = "1",
            };
            PlayFabClientAPI.GetCatalogItems(request, OnGetShopDataSuccessHandler, OnDataRecievedError);
        }

        private void Awake() => ServiceLocator.SharedInstanse.Register<IPlayfabManager>(this);
        private void OnDestroy() => ServiceLocator.SharedInstanse.Unregister<IPlayfabManager>();
        private void OnDataResieved(GetUserDataResult data)
        {
            Debug.Log("Recieved user data!");
            int coins = 0;
            int goldCoins = 0;
            if (data.Data != null)
            {
                if (data.Data.TryGetValue(PlayerCurrencyType.Coins.ToString(), out var coinsString))
                {
                    if (int.TryParse(coinsString.Value, out var coinsInt))
                    {
                        coins = coinsInt;
                    }
                }

                if (data.Data.TryGetValue(PlayerCurrencyType.GoldCoins.ToString(), out var goldCoinsString))
                {
                    if (int.TryParse(goldCoinsString.Value, out var goldCoinsIng))
                    {
                        goldCoins = goldCoinsIng;
                    }
                }
                _tempPlayerCurrencyData = new PlayerCurrencyData { Coins = coins, GoldCoins = goldCoins };
            }
        }
        private void OnGetShopDataSuccessHandler(GetCatalogItemsResult data)
        {
            Debug.Log("Successfuly recieved items data!");

            var adapter = new ShopItemsAdapter();
            List<UI.BallItemDescriptor> itemsList = new List<UI.BallItemDescriptor>();
            foreach (var item in data.Catalog)
            {
                var itemData = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.CustomData);
                itemsList.Add(adapter.GetBallDescriptor(
                    item.DisplayName
                    , itemData[adapter.ColorKey]
                    , itemData[adapter.PriceKey]
                    , itemData[adapter.WeightKey]
                    , itemData[adapter.RadiusKey]));
            }

            var initShopCommand = Factory.Command.CreateInitGameShopCommands();
            initShopCommand.Execute(itemsList);
        }


        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Successful login / account created");
            GetCurrencyData();
            GetShopItemsData();
        }
        private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Successfull leaderboard sent!");
        }
        private void OnDataSent(UpdateUserDataResult obj)
        {
            Debug.Log("Player data successfully sent!");
        }
        private void OnLoginError(PlayFabError error)
        {
            Debug.Log("Error while login / creating account!");
            Debug.Log(error.GenerateErrorReport());
        }
        private void OnLeaderboardRequesError(PlayFabError error)
        {
            Debug.Log("Error while leaderboard request!");
            Debug.Log(error.GenerateErrorReport());
        }
        private void OnDataSendError(PlayFabError error)
        {
            Debug.Log("Error while sendig player data!");
            Debug.Log(error.GenerateErrorReport());
        }
        private void OnDataRecievedError(PlayFabError error)
        {
            Debug.Log("Error while recieving data!");
            Debug.Log(error.GenerateErrorReport());
        }
    }
}
