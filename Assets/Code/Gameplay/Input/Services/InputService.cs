using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Gameplay.Input.Services
{
    public class InputService : IInputService, IDisposable
    {
        private readonly PlayerInput _playerInput;
        
        private Camera _mainCamera;
        private Vector3 _screenPosition;

        public event Action MoveActionPressed;
        public event Action MoveActionCanceled;
        
        public InputService()
        {
            _playerInput = new PlayerInput();
            
            _playerInput.Enable();

            _playerInput.Hero.Move.performed += OnMoveActionPressed;
            _playerInput.Hero.Move.canceled += OnMoveActionCanceled;
        }
        
        public bool HasAxisInput() => GetAxisInput() != Vector2.zero;
    
        public Vector2 GetAxisInput() => _playerInput.Hero.Move.ReadValue<Vector2>();

        public void Dispose()
        {
            _playerInput.Hero.Move.performed -= OnMoveActionPressed;
            _playerInput.Hero.Move.canceled -= OnMoveActionCanceled;
            
            _playerInput?.Disable();
            _playerInput?.Dispose();
        }

        private void OnMoveActionPressed(InputAction.CallbackContext context) 
            => MoveActionPressed?.Invoke();
        
        private void OnMoveActionCanceled(InputAction.CallbackContext context) 
            => MoveActionCanceled?.Invoke();
    }
}