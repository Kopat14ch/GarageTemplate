using Code.Gameplay.Input.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Services
{
    public interface IRigidbodyMoveService
    {
        void Move(IInputService inputService, float speed);
        void Stop();
        void Destroy();
    }
}