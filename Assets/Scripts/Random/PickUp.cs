using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the object when player walks on it
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 50f); // Make the object spin
    }
}
