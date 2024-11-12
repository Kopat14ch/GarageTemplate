using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Features.DragAndDrop.Services;
using Code.Gameplay.Input.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Gameplay.Features.DragAndDrop.Behaviours
{
    public class DragController : MonoBehaviour
    {
        private IDragService _dragService;
        private IInputService _inputService;
        private ICameraProvider _cameraProvider;
        private const float MaxPickupDistance = 2f;

        [Inject]
        public void Construct(IDragService dragService, IInputService inputService, ICameraProvider cameraProvider)
        {
            _dragService = dragService;
            _inputService = inputService;
            _cameraProvider = cameraProvider;
        }

        private void OnEnable()
        {
            _inputService.MouseLeftPressed += TryStartDragging;
            _inputService.MouseLeftCanceled += StopDragging;
        }

        private void OnDisable()
        {
            _inputService.MouseLeftPressed -= TryStartDragging;
            _inputService.MouseLeftCanceled -= StopDragging;
        }

        private void TryStartDragging()
        {
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            Debug.DrawRay(ray.origin, ray.direction * MaxPickupDistance, Color.green, 2f);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxPickupDistance))
            {
                Debug.Log(hitInfo.collider.name);
                
                DraggableItemBehaviour draggableItem = hitInfo.collider.GetComponent<DraggableItemBehaviour>();
                
                if (draggableItem != null)
                {
                    _dragService.StartDragging(draggableItem);
                }
            }
        }

        private void StopDragging()
        {
            _dragService.StopDragging();
        }
    }
}