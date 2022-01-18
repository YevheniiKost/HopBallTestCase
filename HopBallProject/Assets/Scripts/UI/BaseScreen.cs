using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IView
    {
        void Show();
        void Hide();
    }

    public abstract class BaseScreen : MonoBehaviour, IView
    {
        [SerializeField] private bool _hideOnAwake = true;
        public abstract ScreenType ScreenType { get; }
        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnStart()
        {
        }

        public void Show()
        {
            OnShow();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            OnStart();
            EventAggregator.Post(this, new Events.RegisterWindowEvent { ScreenType = this.ScreenType, View = this });
            if (_hideOnAwake)
                Hide();
        }
    }

    public enum ScreenType
    {
        Login,
        Main,
        Shop,
        Win,
        Loading,
        Game,
        PrepareToGame
    }
}
