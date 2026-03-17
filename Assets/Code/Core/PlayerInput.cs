using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Core
{
    public class PlayerInput : MonoBehaviour, Controls.IPlayerActions
    {
        public event Action OnPlayerTouch;
        
        public void OnTouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPlayerTouch?.Invoke();
            }
        }
    }
}