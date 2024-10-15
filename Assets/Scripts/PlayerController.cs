using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA.PlayerController
{
    [DefaultExecutionOrder(-1)]
    public class PlyaerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _playerCamera;

        [Header("Base Movement")]
        public float runAcceleration = 50f;
        public float runSpeed = 4f;
        public float drag = 0.1f;

        private PlayerLocomotioninput _playerLocomotioninput;


        public float turnSmoothTime = 0.1f;
        public float turnSmoothVelocity;


        private void Awake()
        {
            _playerLocomotioninput = GetComponent<PlayerLocomotioninput>();
        }

        private void Update()
        {
            // get the camera forward direction and right directiona nd project it to XZ plane
            // this will get the movement direction based on the camera 
            Vector3 cameraFprwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
            Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
            Vector3 movementDirection = cameraRightXZ * _playerLocomotioninput.MovementInput.x + cameraFprwardXZ * _playerLocomotioninput.MovementInput.y;

            // 
            Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
            Vector3 newVelocity = _characterController.velocity + movementDelta;

            // add drag to player. - to stop continius sliding 
            Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;

            // this will prevent the drag from sending player beckwards. 
            newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
            // Stop the player infinitely accelerating
            newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);

            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Move charactor only once per tick
            _characterController.Move(newVelocity * Time.deltaTime);
        }
    }
}