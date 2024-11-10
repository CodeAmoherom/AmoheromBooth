using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform Cam;

    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 3.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if the player is grounded
        groundedPlayer = controller.isGrounded;

        // Reset vertical velocity when grounded
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f; // Prevent the player from sticking to the ground
        }

        // Handle jump input
        if (groundedPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue); // Jump formula
        }

        // Handle movement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;

            // Smooth the turn angle
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime
            );

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * playerSpeed * Time.deltaTime);
        }

        // Apply gravity if the player is not grounded
        if (!groundedPlayer)
        {
            playerVelocity.y += gravityValue * Time.deltaTime; // Apply gravity
        }

        // Apply vertical movement (jump or fall)
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
