using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Set the speed of camera movement
    public float cameraSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical input for keyboard controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the current mouse position
        Vector3 mousePos = Input.mousePosition;

        // Get the width and height of the screen
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Set the camera movement direction based on the keyboard input
        float horizontalMovement = horizontalInput;
        float verticalMovement = verticalInput;

        // Check if the mouse is on the left side of the screen
        if (mousePos.x < 100)
        {
            horizontalMovement -= 1f;
        }
        // Check if the mouse is on the right side of the screen
        else if (mousePos.x > screenWidth - 100)
        {
            horizontalMovement += 1f;
        }

        // Check if the mouse is on the top side of the screen
        if (mousePos.y < 100)
        {
            verticalMovement -= 1f;
        }
        // Check if the mouse is on the bottom side of the screen
        else if (mousePos.y > screenHeight - 100)
        {
            verticalMovement += 1f;
        }

        // Normalize the movement vector to prevent faster diagonal movement
        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;

        // Transform the movement from local to global coordinates
        movement = transform.TransformDirection(movement);

        // Zero out the Y component to prevent movement along the Y axis
        movement.y = 0f;

        // Move the camera
        transform.Translate(movement * cameraSpeed * Time.deltaTime, Space.World);
    }
}