using UnityEngine;

namespace Code.Gameplay.Features.Cursor.Behaviour
{
    public class LockCursor : MonoBehaviour
    {
        void Start()
        {
            LockCursorToCenter();
        }

        void Update()
        {
            if (UnityEngine.Cursor.lockState != CursorLockMode.Locked)
            {
                LockCursorToCenter();
            }
        }

        private void LockCursorToCenter()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }

        public void UnlockCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}