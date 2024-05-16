using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float rotationAngle = 45f;
    public float selfDestructDelay = 1.5f;
    public ParticleSystem deathParticleSystemPrefab;
    public AudioSource deathAudioSource;

    public bool doesPath = false;
    public Transform[] waypoints;

    private Quaternion initialRotation;
    private Transform player;
    private EnemyLOS enemyLOS;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        initialRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyLOS = GetComponentInChildren<EnemyLOS>();
        agent = GetComponent<NavMeshAgent>();

        if (doesPath && waypoints.Length > 0)
        {
            StartCoroutine(Patrol());
        }
        else
        {
            StartCoroutine(Idle());
        }
    }

    IEnumerator Idle()
    {
        while (true)
        {
            float currentRotation = Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;
            transform.rotation = initialRotation * Quaternion.Euler(0f, currentRotation, 0f);
            yield return null;
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }

            if (enemyLOS.IsPlayerSeen())
            {
                StartCoroutine(AttackPlayer());
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
            if (!enemyLOS.IsPlayerSeen())
            {
                StartCoroutine(ReturnToPatrol());
                yield break;
            }
            else
            {
                // Add attack behavior here
                Debug.Log("Attacking player!");
            }

            yield return null;
        }
    }

    IEnumerator ReturnToPatrol()
    {
        while (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) > agent.stoppingDistance)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
            yield return null;
        }

        StartCoroutine(Patrol());
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
