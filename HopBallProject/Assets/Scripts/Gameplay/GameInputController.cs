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
        [SerializeField] private JoystickController _leftJoystic;
        [SerializeField] private JoystickController _rightJoystic;

        private IJoysticController _leftJoysticController => _leftJoystic;
        private IJoysticController _rightJoystickController => _rightJoystic;

        private IJoysticController _currentController;
        private Camera _mainCamera;
        private bool _playerInput;


        private void Start()
        {
            _mainCamera = ServiceLocator.SharedInstanse.Resolve<IMainCameraService>().MainCamera;
        }

        private void Update()
        {
            _playerInput = PlayerInput();
            if (!PlayerInput())
            {
                _currentController = null;
            }
        }

        private void FixedUpdate()
        {
            if (_playerInput)
                ProcessInput();
        }

        private void ProcessInput()
        {
            var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, _joysticLayerMask);

            if(_currentController != null)
            {
                _currentController.Move(mouseWorldPosition);
                return;
            }

            if (hit.collider == null)
            {
                return;
            }else if(hit.collider == _leftJoysticController.MyCollider)
            {
                _currentController = _leftJoysticController;
            }else if (hit.collider == _rightJoystic.MyCollider)
            {
                _currentController = _rightJoystickController;
            }
        }

        private bool PlayerInput()
        {
            return Input.GetMouseButton(0);
        }
    }
}
