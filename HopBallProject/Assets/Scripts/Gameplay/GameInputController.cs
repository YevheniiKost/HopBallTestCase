using Core.Services;
using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameInputController : MonoBehaviour
    {
        [SerializeField] private LayerMask _joysticLayerMask;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = ServiceLocator.SharedInstanse.Resolve<IMainCameraService>().MainCamera;
        }

        private void Update()
        {
#if UNITY_EDITOR

            if (Input.GetMouseButton(0))
            {
                ProcessMouseInput();
            }
#endif

#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                foreach (var touch in Input.touches)
                {
                    ProcessTouchInput(touch.position);
                }
            }
#endif
        }

        private void ProcessMouseInput()
        {
            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, _joysticLayerMask);

            if(hit.collider.TryGetComponent<IJoysticController>(out var joystic))
            {
                joystic.Move(mousePosition);
            }
        }

        private void ProcessTouchInput(Vector3 screenPosition)
        {
            var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, _joysticLayerMask);

            if (hit.collider.TryGetComponent<IJoysticController>(out var joystick))
            {
                joystick.Move(mouseWorldPosition);
            }

        }
    }
}
