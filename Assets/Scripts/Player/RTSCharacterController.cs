using UnityEngine;
using UnityEngine.AI;

public class RTSCharecterController : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    private Material originalMaterial;
    private bool isActive = false;
    private NavMeshAgent agent;

    void Start()
    {
        // Get reference to NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        // Store the original material of the object
        originalMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object with the Player tag
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
            {
                // Toggle the object's active state
                ToggleActiveState();
            }
            // If the object is active
            else if (isActive)
            {
                // Use NavMesh to determine the destination
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 100f, NavMesh.AllAreas))
                {
                    // Set the destination for the NavMeshAgent
                    SetDestination(navHit.position);
                }
            }
        }
    }

    // Method to toggle the active state of the player object
    private void ToggleActiveState()
    {
        isActive = !isActive;

        // Change the object's material based on its active state
        if (isActive)
        {
            GetComponent<Renderer>().material = activeMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }

    // Method to set destination for the NavMeshAgent
    private void SetDestination(Vector3 destination)
    {
        // Set the destination for the NavMeshAgent
        agent.SetDestination(destination);
    }

    // Method to handle collision with objects
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.tag);
    }
}
