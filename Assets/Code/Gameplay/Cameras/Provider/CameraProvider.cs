﻿using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
    public class CameraProvider : ICameraProvider
    {
        private Camera _mainCamera;
        
        public Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                    _mainCamera = Camera.main;
                
                return Camera.main;
            }
            private set => _mainCamera = value;
        }

        public float WorldScreenHeight { get; private set; }
        public float WorldScreenWidth { get; private set; }

        public void SetMainCamera(Camera camera)
        {
            _mainCamera = camera;

            RefreshBoundaries();
        }

        private void RefreshBoundaries()
        {
            Vector2 bottomLeft = MainCamera.ViewportToWorldPoint(new Vector3(0, 0, MainCamera.nearClipPlane));
            Vector2 topRight = MainCamera.ViewportToWorldPoint(new Vector3(1, 1, MainCamera.nearClipPlane));
            WorldScreenWidth = topRight.x - bottomLeft.x;
            WorldScreenHeight = topRight.y - bottomLeft.y;
        }
    }
}