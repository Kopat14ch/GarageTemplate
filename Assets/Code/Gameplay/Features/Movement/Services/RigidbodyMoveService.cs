using System;
using System.Threading;
using Code.Gameplay.Input.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Services
{
    public class RigidbodyMoveService : IRigidbodyMoveService
    {
        private readonly Rigidbody _rigidbody;
        
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _token;
        
        private float _speed;
        private bool _isMoving;

        public RigidbodyMoveService(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;
        }
        
        public async void Move(IInputService inputService, float speed)
        {
            if (_isMoving)
                return;
            
            _speed = speed;
            _isMoving = true;
            
            while (_isMoving && _cancellationTokenSource.IsCancellationRequested == false)
            {
                Vector2 direction = inputService.GetAxisInput();
            
                _rigidbody.velocity = new Vector3(direction.x, _rigidbody.velocity.y, direction.y) * _speed;
                
                await UniTask.Yield();
            }
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
            _isMoving = false;
        }

        public void Destroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}
