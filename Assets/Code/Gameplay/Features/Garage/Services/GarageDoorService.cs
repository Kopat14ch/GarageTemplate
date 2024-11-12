using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Features.Garage.Services
{
    public class GarageDoorService
    {
        private const float TimeToOpen = 12f;
        
        private readonly Transform _transformDoor;
        private readonly Vector3 _openPosition;

        private readonly AudioSource _audioSource;

        public GarageDoorService(Transform transformDoor, Collider doorCollider, AudioSource audioSource)
        {
            _audioSource = audioSource;
            _transformDoor = transformDoor;
            
            _openPosition = _transformDoor.position + Vector3.up * doorCollider.bounds.size.y;
        }

        public void Open()
        {
            _transformDoor.DOMove(_openPosition, TimeToOpen).SetEase(Ease.OutCubic);
            _audioSource.Play();
        }
    }
}