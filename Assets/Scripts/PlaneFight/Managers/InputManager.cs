using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneFight
{
    public class InputManager : SingletonAutoMono<InputManager>
    {
        private PlayerInputAction playerInputAction;
        private void Awake()
        {
            playerInputAction = new PlayerInputAction();
            playerInputAction.Enable();
        }

        public Vector2 GetMoveDirection()
        {
            return playerInputAction.Player.Move.ReadValue<Vector2>().normalized;
        }
    }
}
