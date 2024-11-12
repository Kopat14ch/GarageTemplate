using System;
using UnityEngine;

namespace Code.Gameplay.Input.Services
{
    public interface IInputService
    {
        event Action MoveActionPressed;
        event Action MoveActionCanceled;
        
        bool HasAxisInput();
        Vector2 GetAxisInput();
    }
}