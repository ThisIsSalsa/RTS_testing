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

        // Set the camera movement direction based on the keyboard input
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the camera
        transform.Translate(movement * cameraSpeed * Time.deltaTime, Space.World);
    }
}
