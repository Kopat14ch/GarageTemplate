using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Gameplay.Input.Services
{
    public class InputService : IInputService, IDisposable
    {
        private readonly PlayerInput _playerInput;
        private Vector3 _screenPosition;

        public event Action<Vector2> MouseMoved;
        public event Action MoveActionPressed;
        public event Action MoveActionCanceled;
        public event Action MouseLeftPressed;
        public event Action MouseLeftCanceled;
        
        public InputService()
        {
            _playerInput = new PlayerInput();
            
            _playerInput.Enable();

            _playerInput.Hero.Move.performed += OnMoveActionPressed;
            _playerInput.Hero.Move.canceled += OnMoveActionCanceled;
            _playerInput.Hero.MouseDelta.performed += OnMouseMovePerformed;
            _playerInput.Hero.MouseDownPress.performed += OnMouseActionPressed;
            _playerInput.Hero.MouseDownPress.canceled += OnMouseActionCanceled;
        }
        
        public bool HasAxisInput() => GetAxisInput() != Vector2.zero;
    
        public Vector2 GetAxisInput() => _playerInput.Hero.Move.ReadValue<Vector2>();
        
        public void Dispose()
        {
            _playerInput.Hero.Move.performed -= OnMoveActionPressed;
            _playerInput.Hero.Move.canceled -= OnMoveActionCanceled;
            _playerInput.Hero.MouseDelta.performed -= OnMouseMovePerformed;
            _playerInput.Hero.MouseDownPress.performed -= OnMouseActionPressed;
            _playerInput.Hero.MouseDownPress.canceled -= OnMouseActionCanceled;
            
            _playerInput?.Disable();
            _playerInput?.Dispose();
        }
        
        private void OnMouseMovePerformed(InputAction.CallbackContext context)
        {
            Vector2 mouseDelta = context.ReadValue<Vector2>().normalized;
            
            mouseDelta.x = mouseDelta.x;
            
            MouseMoved?.Invoke(mouseDelta); 
        }

        private void OnMoveActionPressed(InputAction.CallbackContext context) 
            => MoveActionPressed?.Invoke();
        
        private void OnMoveActionCanceled(InputAction.CallbackContext context) 
            => MoveActionCanceled?.Invoke();        
        
        private void OnMouseActionPressed(InputAction.CallbackContext context) 
            => MouseLeftPressed?.Invoke();
        
        private void OnMouseActionCanceled(InputAction.CallbackContext context) 
            => MouseLeftCanceled?.Invoke();
        
    }
}