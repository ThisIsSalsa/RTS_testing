using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Define enum for AI states
    public enum AIState
    {
        Idle,
        Chasing,
        Alert,
        Searching,
        Returning
    }

    // Variables for AI behavior
    private AIState currentState;
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Other settings
    public float detectionRange = 10f;
    public float viewAngle = 45f;
    public float moveSpeed = 5f;
    public float idleRotationSpeed = 30f;
    private Vector3 lastKnownPlayerPosition;

    // Searching behavior
    public float searchRadius = 10f;
    
    private bool isMovingToSearchPoint = false;
    private float searchTimer;
    [SerializeField]
    private float searchDuration = 0.000030f;

    void Start()
    {
        // Assume some condition triggers the enemy to enter the idle state
        currentState = AIState.Idle;

        // Get references
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position; // Set initial position
        initialRotation = transform.rotation; // Set initial rotation
    }

    void Update()
    {
        // Update behavior based on current state
        switch (currentState)
        {
            case AIState.Idle:
                IdleBehavior();
                break;
            case AIState.Chasing:
                ChasingBehavior();
                break;
            case AIState.Alert:
                AlertBehavior();
                break;
            case AIState.Searching:
                SearchingBehavior();
                break;
            case AIState.Returning:
                ReturnToInitialPosition();
                break;
        }
    }

    // Idle state behavior
    void IdleBehavior()
    {
        // Perform idle rotation
        float rotationAmount = Mathf.Sin(Time.time * idleRotationSpeed) * 30f;
        transform.rotation = Quaternion.Euler(0f, rotationAmount, 0f);

        // Check for player detection
        if (IsPlayerDetected())
        {
            currentState = AIState.Chasing;
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    // Chasing state behavior
    void ChasingBehavior()
    {
        // Chase the player
        if (player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);

            // Update last known player position
            lastKnownPlayerPosition = player.transform.position;
        }

        // Check for player loss
        if (!IsPlayerDetected())
        {
            currentState = AIState.Alert;
            // Start searching when losing sight of the player
            StartSearching();
        }
    }
        void StartSearching()
    {
        currentState = AIState.Searching;
        searchTimer = searchDuration;
    }

    // Searching state behavior
void SearchingBehavior()
{
    // Check if the AI is already moving to a search point
    if (!isMovingToSearchPoint)
    {
        // Move randomly within the search area
        if (searchTimer <= 0f)
        {
            // End searching after timeout
            Debug.Log("Search timer expired. Transitioning to Returning state.");
            currentState = AIState.Returning; // Start returning after searching
            navMeshAgent.SetDestination(initialPosition); // Start returning to initial position
            return;
        }

        // Decrement search timer
        searchTimer -= Time.deltaTime;

        // Your existing code for selecting a random search point here...
        Vector3 randomPoint = GetRandomPointInSearchArea();
        navMeshAgent.SetDestination(randomPoint);
        isMovingToSearchPoint = true;

        // Log remaining search time
        Debug.Log("Remaining search time: " + searchTimer);
    }
    else
    {
        // Your existing code for checking if the AI has reached the current search point here...
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            isMovingToSearchPoint = false;
        }
    }
}




    Vector3 GetRandomPointInSearchArea()
    {
        Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, searchRadius, NavMesh.AllAreas);
        return hit.position;
    }

    // Alert state behavior
    void AlertBehavior()
    {
        // Move back to initial position
        navMeshAgent.SetDestination(initialPosition);

        // Check if reached initial position
        if (Vector3.Distance(transform.position, initialPosition) < 1f)
        {
            currentState = AIState.Idle;
        }
    }

    // Check if player is detected within detection range and angle
    bool IsPlayerDetected()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            return directionToPlayer.magnitude <= detectionRange && Mathf.Abs(angleToPlayer) <= viewAngle * 0.5f;
        }
        return false;
    }

    // Coroutine to return to initial position
    void ReturnToInitialPosition()
    {
        navMeshAgent.SetDestination(initialPosition);
        // Check if reached initial position
        if (Vector3.Distance(transform.position, initialPosition) < 1f)
        {
            currentState = AIState.Idle;
        }
    }
}
