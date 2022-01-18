using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Core.Utilities;
using System;

namespace Core.Backend
{
    public interface IPlayfabManager
    {
        void Login();
    }
    public class PlayfabManager : MonoBehaviour, IPlayfabManager
    {
        public void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
        }

        private void Awake() => ServiceLocator.SharedInstanse.Register<IPlayfabManager>(this);
        private void OnDestroy() => ServiceLocator.SharedInstanse.Unregister<IPlayfabManager>();

        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Successful login / account created");
        }

        private void OnLoginError(PlayFabError error)
        {
            Debug.Log("Error while login / creating account!");
            Debug.Log(error.GenerateErrorReport());
        }
    }
}
