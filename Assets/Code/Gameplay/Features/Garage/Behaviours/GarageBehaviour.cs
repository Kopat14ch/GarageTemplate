using System;
using Code.Gameplay.Features.Garage.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Garage.Behaviours
{
    public class GarageBehaviour : MonoBehaviour
    {
        [SerializeField] private Collider _doorCollider;
        [SerializeField] private Transform _doorTransform;
        [SerializeField] private AudioSource _doorAudioSource;

        private void Awake()
        {
            new GarageDoorService(_doorTransform, _doorCollider, _doorAudioSource).Open();
        }
    }
}