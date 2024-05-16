using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemyLOS : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Material normalMaterial;
    public Material alertMaterial;
    public bool VisionActive = false;

    private Transform playerTransform;
    private new Renderer renderer;
    private bool playerInLOS = false;
    private bool activated = false;

    public UnityEvent onActivateMesh;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        renderer = GetComponent<Renderer>();
        renderer.material = normalMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInLOS = true;
            if (!activated)
            {
                renderer.material = alertMaterial;
                renderer.enabled = true;
                activated = true;
                onActivateMesh.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInLOS = false;
        }
    }

    public bool IsPlayerSeen()
    {
        return playerInLOS;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerTransform.position;
    }

    public void ActivateMesh()
    {
        StartCoroutine(WaitForHover());
    }

    public IEnumerator WaitForHover()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        if (!activated)
        {
            renderer.enabled = true;
            activated = true;
            onActivateMesh.Invoke();
        }
        else
        {
            renderer.enabled = false;
            activated = false;
        }
    }
}
