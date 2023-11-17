using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneFight
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        private PlayerInputAction playerInputAction;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
            playerInputAction = new PlayerInputAction();
            playerInputAction.Enable();
        }

        public Vector2 GetMoveDirection()
        {
            return playerInputAction.Player.Move.ReadValue<Vector2>().normalized;
        }
    }
}
