using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public GameObject questPanelPrefab; // Reference to the quest panel prefab
    public Canvas mainUICanvas; // Reference to the main UI canvas
    public GameObject questMarker; // Reference to the quest marker object

    private GameObject questPanelInstance; // Instance of the quest panel

    private bool questActivated = false; // Flag to track if the quest has been activated
    private bool questCompleted = false; // Flag to track if the quest is completed

    private int cheeseCount = 0; // Example of a quest-related variable

    private void OnCollisionEnter(Collision collision)
    {
        if (!questActivated && !questCompleted && collision.gameObject.CompareTag("Player"))
        {
            ActivateQuest();
        }
    }

    private void Update()
    {
        if (!questCompleted && questMarker != null)
        {
            questMarker.transform.Rotate(Vector3.forward * Time.deltaTime * 50f); // Make the marker spin in the x-axis
        }
    }

    private void ActivateQuest()
    {
        if (questPanelPrefab != null && mainUICanvas != null)
        {
            questPanelInstance = Instantiate(questPanelPrefab, mainUICanvas.transform);
            questPanelInstance.transform.localPosition = Vector3.zero; // Set position to center of canvas
            questActivated = true;
            ToggleQuestMarker(false);
        }
        else
        {
            Debug.LogError("Quest panel prefab or main UI canvas reference not set!");
        }
    }

    public void ButtonYesDown()
    {
        if (questActivated && !questCompleted)
        {
            // Simulate quest completion condition (e.g., collecting cheese)
            cheeseCount += 1;
            if (cheeseCount >= 3)
            {
                CompleteQuest();
            }
        }
    }

    private void CompleteQuest()
    {
        // Additional actions upon quest completion
        questCompleted = true;
        Destroy(questMarker);
    }

    private void ToggleQuestMarker(bool isActive)
    {
        if (questMarker != null)
        {
            questMarker.SetActive(isActive);
        }
    }
    
}
