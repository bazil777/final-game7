using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 10f; //speed of player
    public float jumpForce = 5f; // jump of player
    public float jumpDuration = 0.5f; // how long jump will last
    private Vector3 movement;
    private float verticalVelocity = 0f;
    private bool isJumping = false;
    public float sensitivity = 2.0f; // Mouse look sensitivity.
    private float rotationX = 0;

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement direction based on the current character rotation.
        movement = transform.TransformDirection(Vector3.forward) * verticalInput * speed;

        // Rotate the character to face left when the left arrow key is pressed.
        if (horizontalInput < 0)
        {
            transform.Rotate(Vector3.up, -90f * Time.deltaTime);
        }
        // Rotate the character to face right when the right arrow key is pressed.
        else if (horizontalInput > 0)
        {
            transform.Rotate(Vector3.up, 90f * Time.deltaTime);
        }
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * sensitivity; // Invert the vertical mouse input.

        rotationX += mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Clamp the vertical rotation to prevent buggy flipping

        // Rotate the camera and character separately for looking around.
        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        if (characterController.isGrounded)
        {
            // if player is grounded let him jump
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                verticalVelocity = jumpForce;
            }
        }

        // makes sure my player doesnt perform a jump and float , had an issue but it seems to be resolved with this
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Check if the capsule is in the air and jumping
        if (isJumping)
        {
            movement.y = verticalVelocity;
        }
        else
        {
            //otherwise my player need to be grounded
            movement.y = verticalVelocity;
        }

        // Move the character using the CharacterController.
        characterController.Move(movement * Time.deltaTime);

        // Check if the character has landed after a jump.
        if (characterController.isGrounded)
        {
            isJumping = false;
            // Ensure that the character rests on the ground
            movement.y = 0f;
        }
    }
}


