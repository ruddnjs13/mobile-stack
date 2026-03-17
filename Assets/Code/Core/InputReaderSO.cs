using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Core
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "SO/Input Reader", order = 0)]
    public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnPlayerTouch;
        
        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnTouch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnPlayerTouch?.Invoke();
            }
        }
    }
}