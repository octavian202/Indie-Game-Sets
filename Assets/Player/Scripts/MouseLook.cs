using UnityEngine;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {

        #region Variabile

        public float mouseSensitivity = 700f;
        
        public Transform cameraTransform;
        private float _bobSpeed = 0;

        private float xRotation = 0f;

        private FirstPersonController fpc;

        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            fpc = GetComponent<FirstPersonController>();
        }

        private void Update()
        {
            var inputMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            inputMouse = mouseSensitivity * Time.deltaTime * inputMouse;

            xRotation -= inputMouse.y;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
    
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * inputMouse.x);

            HeadBobMovement();
            
        }

        #region Methods

        private void HeadBobMovement()
        {
            _bobSpeed += Time.deltaTime;
            if (fpc.isIdle) HeadBob(_bobSpeed, 0.1f, 0.1f);
            else if (fpc.isWalking) HeadBob(3f * _bobSpeed, 0.18f, 0.18f);
            else if (fpc.isRunning) HeadBob(4.5f * _bobSpeed, 0.24f, 0.24f);
        }

        private void HeadBob(float fx, float xIntensity, float yIntensity)
        {
            var targetBobPos = new Vector3(Mathf.Cos(fx) * xIntensity, Mathf.Sin(2 * fx) * yIntensity + 0.9f, 0f);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetBobPos, Time.deltaTime * 8f);
        }

        #endregion
        
    }
}