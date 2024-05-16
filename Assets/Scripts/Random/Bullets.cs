using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float selfDestructDelay = 3f; // Delay before self-destructing after collision

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructDelay);
        Destroy(gameObject);
    }
}