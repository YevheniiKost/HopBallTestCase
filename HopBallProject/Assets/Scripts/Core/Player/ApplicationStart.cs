using Core.Player;
using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ApplicationStart : MonoBehaviour
    {
        private IPlayer _player;

        private void Awake()
        {
            _player = Core.Player.Player.Default();
            ServiceLocator.SharedInstanse.Register<IPlayer>(_player);
        }
    }
}