using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public List<GameObject> doors;
    public Transform playerDestination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find out which door trigger the player entered
            GameObject doorTrigger = other.gameObject;

            if (doors.Contains(doorTrigger))
            {
                MovePlayerInsideHouse(other.gameObject);
            }
        }
    }

    private void MovePlayerInsideHouse(GameObject player)
    {
        if (playerDestination != null)
        {
            Debug.Log("Moving player inside the house...");

            // Move the player directly to the destination position
            player.transform.position = playerDestination.position;
        }
        else
        {
            Debug.LogWarning("Player destination is not set for the house: " + gameObject.name);
        }
    }
}
