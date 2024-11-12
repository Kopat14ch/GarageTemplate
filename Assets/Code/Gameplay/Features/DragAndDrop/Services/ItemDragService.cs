using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Features.DragAndDrop.Behaviours;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.DragAndDrop.Services
{
    public class DragService : IDragService, ITickable
    {
        private readonly ICameraProvider _cameraProvider;
        private DraggableItemBehaviour _currentItem;
        private Vector3 _dragOffset;
        private Rigidbody _currentItemRigidbody;
        private bool _wasKinematic;

        public DragService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public void StartDragging(DraggableItemBehaviour item)
        {
            _currentItem = item;

            _currentItemRigidbody = _currentItem.GetRigidbody();
            _currentItemRigidbody.useGravity = false;
            
            _dragOffset = _currentItem.transform.position - _cameraProvider.MainCamera.transform.position;
        }

        public void StopDragging()
        {
            if (_currentItem != null)
            {

                if (_currentItemRigidbody != null)
                {
                    _currentItemRigidbody.isKinematic = _wasKinematic;
                    _currentItemRigidbody.useGravity = true;
                    _currentItemRigidbody = null;
                }
                
                _currentItem = null;
            }
        }

        public void Tick()
        {
            if (_currentItem != null && _currentItemRigidbody != null)
            {
                Ray ray = new Ray(_cameraProvider.MainCamera.transform.position, _cameraProvider.MainCamera.transform.forward);
                
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    Vector3 direction = (hitInfo.point - _cameraProvider.MainCamera.transform.position).normalized;
                    Vector3 targetPosition = _cameraProvider.MainCamera.transform.position + direction * _dragOffset.magnitude;
                    
                    Vector3 velocity = (targetPosition - _currentItemRigidbody.position) / Time.fixedDeltaTime;
                    _currentItemRigidbody.velocity = velocity;
                    
                    float maxVelocity = 20f;
                    
                    if (_currentItemRigidbody.velocity.magnitude > maxVelocity)
                        _currentItemRigidbody.velocity = _currentItemRigidbody.velocity.normalized * maxVelocity;
                    
                }
                else
                {
                    _currentItemRigidbody.velocity = Vector3.zero;
                }
            }
        }
    }
}