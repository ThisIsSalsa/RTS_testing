using UnityEngine;

public class ActivateMenu : MonoBehaviour
{
    public GameObject menuPrefab; 

    // This method will be called when the button is clicked
    public void OnButtonClick()
    {
        // Get the Canvas component attached to the same GameObject
        Canvas canvas = GetComponent<Canvas>();
        // Calculate the center position of the Canvas in world space
        Vector3 centerPosition = canvas.transform.position;
        // Instantiate the menu prefab at the center position of the Canvas
        GameObject menuInstance = Instantiate(menuPrefab, centerPosition, Quaternion.identity, canvas.transform);
        // Adjust the position
        menuInstance.transform.localPosition = Vector3.zero;
    }
}
