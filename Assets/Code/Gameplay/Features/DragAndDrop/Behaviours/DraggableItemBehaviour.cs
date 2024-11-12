using UnityEngine;

namespace Code.Gameplay.Features.DragAndDrop.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class DraggableItemBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public Rigidbody GetRigidbody() => _rigidbody;
    }
}