using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float rotationAngle = 45f;
    public float selfDestructDelay = 1.5f;
    public bool doesPath = false;
    public List<Transform> waypoints;
    public ParticleSystem deathParticleSystemPrefab; 
    public AudioSource deathAudioSource;

    private NavMeshAgent agent;
    private Transform player;
    private EnemyLOS enemyLOS;
    private Vector3 initialPosition;
    private Quaternion initialRotation; // Added
    private int currentWaypointIndex = 0;
    private Coroutine currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyLOS = GetComponentInChildren<EnemyLOS>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position; // Store initial position
        initialRotation = transform.rotation; // Store initial rotation

        currentState = StartCoroutine(FSM());
    }

    IEnumerator FSM()
    {
        while (true)
        {
            if (enemyLOS.IsPlayerSeen())
            {
                currentState = StartCoroutine(Chasing());
            }
            else if (doesPath && waypoints != null && waypoints.Count > 0)
            {
                currentState = StartCoroutine(Patrolling());
            }
            else
            {
                currentState = StartCoroutine(Idling());
            }

            yield return null;
        }
    }

    IEnumerator Patrolling()
    {
        while (true)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            }

            if (enemyLOS.IsPlayerSeen())
            {
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Chasing()
    {
        while (true)
        {
            agent.SetDestination(player.position);

            if (!enemyLOS.IsPlayerSeen())
            {
                agent.SetDestination(initialPosition);
                yield return StartCoroutine(ReturnToInitialPosition());
            }

            yield return null;
        }
    }

    IEnumerator ReturnToInitialPosition()
    {
        while (Vector3.Distance(transform.position, initialPosition) > agent.stoppingDistance)
        {
            yield return null;
        }
    }

    IEnumerator Idling()
    {
        float idleTimer = 0f;

        while (idleTimer < 3f)
        {
            // Calculate the rotation angle based on sine function
            float currentRotation = Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;

            // Set the rotation around the Y-axis (horizontal)
            transform.rotation = initialRotation * Quaternion.Euler(0f, currentRotation, 0f); // Use initial rotation

            idleTimer += Time.deltaTime;

            if (enemyLOS.IsPlayerSeen())
            {
                yield break;
            }

            yield return null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HandleBulletHit();
        }
    }

    public void HandleBulletHit()
    {
        StartCoroutine(SelfDestruct());
    }

    public void HandleDashHit()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        ParticleSystem particleSystemInstance = Instantiate(deathParticleSystemPrefab, transform.position, Quaternion.identity);
        particleSystemInstance.Play();
        deathAudioSource.Play();
        yield return new WaitForSeconds(selfDestructDelay);
        Destroy(gameObject);
        yield return new WaitForSeconds(3f);
        deathAudioSource.Stop();
        particleSystemInstance.Clear();
        particleSystemInstance.Stop();
        Destroy(particleSystemInstance.gameObject);
    }
}
