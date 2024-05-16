using UnityEngine;

public enum ItemType
{
    Ammo,
    Food,
    Gold
}

public class PickUp : MonoBehaviour
{
    public ItemType itemType = ItemType.Ammo; // Select the item type in the Inspector
    public int itemCount = 1; // Change this to adjust how much of the item is added to the inventory

    public float amplitude = 0.5f;
    public float frequency = 1f;
    private Vector3 startPos;

    public AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddToInventory(collision.gameObject);
            PlayPickUpSound(); // Play the pickup sound
            Destroy(gameObject);
        }
    }

    private void AddToInventory(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory != null)
        {
            string itemTag = itemType.ToString(); // Convert enum value to string
            inventory.AddItem(itemTag, itemCount);
        }
        else
        {
            Debug.LogWarning("Player does not have an Inventory script attached.");
        }
    }

    private void PlayPickUpSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // Play the assigned audio clip
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 50f);
        Vector3 newPosition = startPos + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = newPosition;
    }
}
