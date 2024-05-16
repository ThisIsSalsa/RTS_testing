using UnityEngine;
using UnityEngine.AI;

public class RTSCharecterController : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    private Material originalMaterial;
    private bool isActive = false;
    private NavMeshAgent agent;

    // Variables for double-click detection
    private float lastClickTime = 0f;
    private float doubleClickTimeThreshold = 0.3f;

    // Movement speed variables
    private float normalSpeed = 5f;
    private float sprintSpeed = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
            {
                ToggleActiveState();
            }
            else if (isActive)
            {
                if (Time.time - lastClickTime <= doubleClickTimeThreshold)
                {
                    IncreaseMoveSpeed();
                }
                else
                {
                    MoveToDestination(hit.point);
                }
                lastClickTime = Time.time;
            }
        }
    }

    private void ToggleActiveState()
    {
        isActive = !isActive;
        if (isActive)
        {
            GetComponent<Renderer>().material = activeMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }

    private void MoveToDestination(Vector3 destination)
    {
        agent.speed = normalSpeed;
        agent.SetDestination(destination);
    }

    private void IncreaseMoveSpeed()
    {
        switch (agent.speed)
        {
            case 5f:
                agent.speed = sprintSpeed;
                break;
            case 10f:
                agent.speed = normalSpeed;
                break;
        }
    }
}
