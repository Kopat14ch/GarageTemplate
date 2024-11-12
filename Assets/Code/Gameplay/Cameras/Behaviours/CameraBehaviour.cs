using Code.Gameplay.Features.Rotating.Services;
using Code.Gameplay.Input.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Cameras.Behaviours
{
    public class CameraBehaviour : MonoBehaviour
    {
        private IRotateWitchMouseDeltaService _rotateService;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService, IRotateWitchMouseDeltaService rotateService)
        {
            _inputService = inputService;
            _rotateService = rotateService;

            _rotateService.SetTransform(transform);
        }
        
        private void OnEnable() =>
            _inputService.MouseMoved += OnMouseMoved;

        private void OnDisable() =>
            _inputService.MouseMoved -= OnMouseMoved;

        private void OnMouseMoved(Vector2 mouseDelta)
        {
            _rotateService.RotateX(mouseDelta.y);
        }
    }
}