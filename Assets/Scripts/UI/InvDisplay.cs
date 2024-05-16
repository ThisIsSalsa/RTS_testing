using UnityEngine;
using UnityEngine.UI;

public class InvDisplay : MonoBehaviour
{
    public Text foodText;
    public Text goldText;
    public Text ammoText;
    public Inventory playerInventory; // Reference to the player's inventory

    private void Update()
    {
        if (playerInventory != null)
        {
            UpdateInventoryDisplay();
        }
    }

    private void UpdateInventoryDisplay()
    {
        if (foodText != null)
        {
            foodText.text = "Food: " + playerInventory.GetItemCount("Food");
        }
        if (goldText != null)
        {
            goldText.text = "Gold: " + playerInventory.GetItemCount("Gold");
        }
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + playerInventory.GetItemCount("Ammo");
        }
    }
}
