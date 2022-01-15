using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utilities

{
    public interface IServiceLocator
    {
        void Register<T>(T data);
        void Unregister<T>();
        T Resolve<T>();
    }

    public class ServiceLocator : IServiceLocator
    {
        private static IServiceLocator _instance;
        public static IServiceLocator SharedInstanse
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceLocator();
                }
                return _instance;
            }
        }

        private readonly Dictionary<Type, object> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<Type, object>();
        }

        public void Register<T>(T data)
        {
            if (_services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service {typeof(T).Name} already registered");
            }
            else
            {
                _services[typeof(T)] = data;
                Debug.Log($"Service {typeof(T).Name} was registered");
            }
        }

        public T Resolve<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service {typeof(T).Name} did't registered");
            }
            else
            {
                return (T)_services[typeof(T)];
            }

            return default;
        }

        public void Unregister<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service {typeof(T).Name} already removed");
            }
            else
            {
                _services.Remove(typeof(T));
            }
        }
    }
}