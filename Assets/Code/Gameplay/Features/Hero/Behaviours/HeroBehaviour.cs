using Code.Gameplay.Features.Hero.Configs;
using Code.Gameplay.Features.Movement.Services;
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

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _moveService = new RigidbodyMoveService(_rigidbody);
        }

        private void OnEnable()
        {
            _inputService.MoveActionPressed += OnMoveActionPressed;
            _inputService.MoveActionCanceled += OnMoveActionCanceled;
        }

        private void OnDisable()
        {
            _inputService.MoveActionPressed -= OnMoveActionPressed;
            _inputService.MoveActionCanceled -= OnMoveActionCanceled;
        }

        private void Update()
        {
            Debug.Log(_rigidbody.velocity);
        }

        private void OnDestroy()
        {
            _moveService.Destroy();
        }


        private void OnMoveActionPressed()
        {
            _moveService.Move(_inputService, _heroConfig.MoveSpeed);
        }

        private void OnMoveActionCanceled()
        {
            _moveService.Stop();
        }
    }
}