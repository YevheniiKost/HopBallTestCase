using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    public class MainCameraService : MonoBehaviour, IMainCameraService
    {
        [SerializeField] private Camera _camera;
        public Camera MainCamera => _camera;

        private void Awake()
        {
            ServiceLocator.SharedInstanse.Register<IMainCameraService>(this);   
        }

        private void OnDestroy()
        {
            ServiceLocator.SharedInstanse.Unregister<IMainCameraService>();
        }
    }

    public interface IMainCameraService
    {
        Camera MainCamera { get; }
    }
}