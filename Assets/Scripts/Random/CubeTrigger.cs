using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    public Transform playerDestination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (playerDestination != null)
        {
            Debug.Log("Teleporting player...");

            // Teleport the player to the destination position
            player.transform.position = playerDestination.position;
        }
        else
        {
            Debug.LogWarning("Player destination is not set for the teleport trigger: " + gameObject.name);
        }
    }
}
