using UnityEngine;
using UnityEngine.UI;

public class SkillTabMove : MonoBehaviour
{
    public RectTransform panelRectTransform;
    public float moveDistance = 100f;
    private bool isPanelMovedUp = false;

    // Method to handle button click
    public void OnButtonClick()
    {
        if (isPanelMovedUp)
        {
            // If the panel is currently moved up, move it back down
            MovePanel(-moveDistance);
        }
        else
        {
            // If the panel is currently not moved up, move it up
            MovePanel(moveDistance);
        }
    }

    // Method to move the panel by the specified distance
    private void MovePanel(float distance)
    {
        // Calculate the new position of the panel
        Vector3 newPosition = panelRectTransform.localPosition + new Vector3(0f, distance, 0f);

        // Set the new position of the panel
        panelRectTransform.localPosition = newPosition;

        // Update the flag to indicate the panel's movement status
        isPanelMovedUp = !isPanelMovedUp;
    }
}
