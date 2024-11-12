using UnityEngine;

namespace Code.Gameplay.Features.Rotating.Services
{
    public class RotateWitchMouseDeltaService : IRotateWitchMouseDeltaService
    {
        private const float VerticalAngle = 89f;
        private const float RotateSpeed = .5f;

        private Transform _rotateTransform;
        private float _rotationX;
        private float _rotationY;

        public void SetTransform(Transform rotateTransform)
        {
            _rotateTransform = rotateTransform;
        }
        
        public void RotateX(float deltaY)
        {
            _rotationX += deltaY;
            _rotationX = Mathf.Clamp(_rotationX, -VerticalAngle, VerticalAngle);
            ApplyRotation();
        }
        
        public void RotateY(float deltaX)
        {
            _rotationY += deltaX;
            ApplyRotation();
        }

        public void RotateXY(Vector2 mouseDelta)
        {
            RotateX(mouseDelta.y);
            RotateY(mouseDelta.x);
        }
        
        private void ApplyRotation()
        {
            Quaternion targetRotation = Quaternion.Euler(-_rotationX, _rotationY, 0f);
            
            _rotateTransform.localRotation = targetRotation;
        }
    }
}