using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
    public class CameraProvider : ICameraProvider
    {
        public Camera MainCamera
        {
            get
            {
                if (MainCamera == null)
                    MainCamera = Camera.main;
                
                return Camera.main;
            }
            private set => MainCamera = value;
        }

        public float WorldScreenHeight { get; private set; }
        public float WorldScreenWidth { get; private set; }

        public void SetMainCamera(Camera camera)
        {
            MainCamera = camera;

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