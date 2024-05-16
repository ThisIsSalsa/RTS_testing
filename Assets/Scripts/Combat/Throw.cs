using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throw : MonoBehaviour
{
    public GameObject ammoPrefab; // Reference to the prefab of the object you want to throw
    public Transform spawnPoint; // Reference to the spawn point where the object will be instantiated

    private bool throwActivated = false; // Flag to control when to throw

    void Update()
    {
        if (throwActivated)
        {
            RotateCharacterTowardsMouse();

            if (Input.GetMouseButtonDown(0)) // Check if the throw is activated and left mouse button is clicked
            {
                ThrowObject();
            }
            else if (Input.GetKeyDown(KeyCode.Space)) // Check if space bar is pressed to cancel throw
            {
                throwActivated = false;
            }
        }
    }

    public void ActivateThrow()
    {
        throwActivated = true;
        StartCoroutine(WaitForDeactivation());
    }

    IEnumerator WaitForDeactivation()
    {
        yield return null;
        while (throwActivated)
        {
            yield return null;
        }
    }

    void RotateCharacterTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep the same y position as the character
            transform.LookAt(targetPosition);
        }
    }

    void ThrowObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - spawnPoint.position; // Calculate direction towards the hit point
            direction.Normalize(); // Normalize the direction vector

            GameObject ammoInstance = Instantiate(ammoPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = ammoInstance.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply force towards the hit point
                rb.AddForce(direction * 60f, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody component not found on the thrown object.");
            }
        }

        throwActivated = false; // Reset the flag after throwing
    }
}
