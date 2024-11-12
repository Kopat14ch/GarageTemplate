using System;
using UnityEngine;

namespace Code.Gameplay.Input.Services
{
    public interface IInputService
    {
        event Action<Vector2> MouseMoved;
        event Action MoveActionPressed;
        event Action MoveActionCanceled;
        event Action MouseLeftPressed;
        event Action MouseLeftCanceled;
        
        bool HasAxisInput();
        Vector2 GetAxisInput();
    }
}