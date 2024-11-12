using UnityEngine;

namespace Code.Gameplay.Features.Rotating.Services
{
    public interface IRotateWitchMouseDeltaService
    {
        void SetTransform(Transform rotateTransform);
        void RotateX(float deltaY);
        void RotateY(float deltaX);
    }
}