using Core.Utilities;
using Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IUIManager
    {
        void OpenScreen(ScreenType type);
        void CloseScreen(ScreenType type);
        IView GetWindow(ScreenType type);
    }

    public class UIManager : MonoBehaviour, IUIManager
    {
        private Dictionary<ScreenType, IView> _screens = new Dictionary<ScreenType, IView>();

        public void CloseScreen(ScreenType type)
        {
            if(_screens.TryGetValue(type, out var screen))
            {
                screen.Hide();
            }
            else
            {
                DoNotContainScreenError(type.ToString());
            }
        }

        public void OpenScreen(ScreenType type)
        {
            if (_screens.TryGetValue(type, out var screen))
            {
                screen.Show();
            }
            else
            {
                DoNotContainScreenError(type.ToString());
            }
        }

        public IView GetWindow(ScreenType type)
        {
            if(_screens.TryGetValue(type, out var screen))
            {
                return screen;
            }
            else
            {
                DoNotContainScreenError(type.ToString());
            }
                return null;
        }

        private void Awake()
        {
            ServiceLocator.SharedInstanse.Register<IUIManager>(this);
            EventAggregator.Subscribe<Events.RegisterWindowEvent>(OnRegisterWindow);
        }

        private void OnDestroy()
        {
            ServiceLocator.SharedInstanse.Unregister<IUIManager>();
            EventAggregator.Unsubscribe<Events.RegisterWindowEvent>(OnRegisterWindow);
        }

        private void OnRegisterWindow(object arg1, RegisterWindowEvent data)
        {
            if (_screens.ContainsKey(data.ScreenType))
            {
                Debug.LogError($"UI manager already contains window of type {data.ScreenType}!");
                return;
            }

            _screens.Add(data.ScreenType, data.View);
        }

        private void DoNotContainScreenError(string screen) => Debug.LogError($"UI manager do not contain window of type {screen}!");

       
    }
}