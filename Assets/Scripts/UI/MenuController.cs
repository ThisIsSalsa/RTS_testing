using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPrefab; 

    // This method will be called when the "Continue" button is clicked
    public void OnContinueButtonClick()
    {
        Debug.Log("Pressed");
        DestroyMenuPrefab();
    }

    // This method will be called when the Escape key is pressed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Destroy the menu prefab
            DestroyMenuPrefab();
        }
    }

    // Method to destroy the menu prefab
    private void DestroyMenuPrefab()
    {
        if (menuPrefab != null)
        {
            Destroy(menuPrefab);
        }
    }
}
