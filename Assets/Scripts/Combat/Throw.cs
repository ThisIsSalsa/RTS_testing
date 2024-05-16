using UnityEngine;
using System.Collections;
public class Throw : MonoBehaviour
{
    public GameObject ammoPrefab;
    public Transform spawnPoint;
    private bool throwActivated = false;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>(); // Get the Inventory component attached to the same GameObject
    }

    void Update()
    {
        if (throwActivated)
        {
            RotateCharacterTowardsMouse();

            if (Input.GetMouseButtonDown(0))
            {
                ThrowObject();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
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
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
    }

    void ThrowObject()
    {
        if (inventory.HasItem("Ammo")) // Check if there's ammo in the inventory
        {
            // Remove one unit of ammo from the inventory
            inventory.RemoveItem("Ammo", 1);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = hit.point - spawnPoint.position;
                direction.Normalize();

                GameObject ammoInstance = Instantiate(ammoPrefab, spawnPoint.position, Quaternion.identity);
                Rigidbody rb = ammoInstance.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddForce(direction * 60f, ForceMode.Impulse);
                }
                else
                {
                    Debug.LogError("Rigidbody component not found on the thrown object.");
                }
            }

            throwActivated = false;
        }
        else
        {
            Debug.Log("Out of ammo!"); // Output a message if no ammo is available
        }
    }
}
