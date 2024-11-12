using System.Threading;
using Code.Gameplay.Input.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Services
{
    public class RigidbodyMoveService : IRigidbodyMoveService
    {
        private readonly Rigidbody _rigidbody;
        private readonly CancellationTokenSource _cancellationTokenSource;
        
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

            while (_isMoving && !_cancellationTokenSource.IsCancellationRequested)
            {
                Vector2 direction = inputService.GetAxisInput();
                
                Vector3 moveDirection = _rigidbody.transform.forward * direction.y + _rigidbody.transform.right * direction.x;
                
                _rigidbody.velocity = new Vector3(moveDirection.x, _rigidbody.velocity.y, moveDirection.z) * _speed;
                
                await UniTask.Yield();
            }
        }

        public void Stop()
        {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
            _isMoving = false;
        }

        public void Destroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}
