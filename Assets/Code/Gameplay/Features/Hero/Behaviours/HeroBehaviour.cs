using Code.Gameplay.Features.Hero.Configs;
using Code.Gameplay.Features.Movement.Services;
using Code.Gameplay.Features.Rotating.Services;
using Code.Gameplay.Input.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Hero.Behaviours
{
    public class HeroBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private HeroConfig _heroConfig;
        
        private IRigidbodyMoveService _moveService;
        private IInputService _inputService;
        private IRotateWitchMouseDeltaService _rotateService;

        [Inject]
        public void Construct(IInputService inputService, IRotateWitchMouseDeltaService rotateService)
        {
            _rotateService = rotateService;
            _inputService = inputService;
        }

        private void Awake()
        {
            _moveService = new RigidbodyMoveService(_rigidbody);
            
            _rotateService.SetTransform(_rigidbody.transform);
        }

        private void OnEnable()
        {
            _inputService.MoveActionPressed += OnMoveActionPressed;
            _inputService.MoveActionCanceled += OnMoveActionCanceled;
            _inputService.MouseMoved += OnMouseMoved;
        }

        private void OnDisable()
        {
            _inputService.MoveActionPressed -= OnMoveActionPressed;
            _inputService.MoveActionCanceled -= OnMoveActionCanceled;
            _inputService.MouseMoved -= OnMouseMoved;
        }

        private void OnDestroy()
        {
            _moveService.Destroy();
        }
        
        private void OnMoveActionPressed()
        {
            _moveService.Move(_inputService, _heroConfig.MoveSpeed);
        }

        private void OnMouseMoved(Vector2 mouseDelta)
        {
            _rotateService.RotateY(mouseDelta.x);
        }

        private void OnMoveActionCanceled()
        {
            _moveService.Stop();
        }
    }
}