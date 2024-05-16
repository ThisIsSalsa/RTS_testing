using UnityEngine;
using System.Collections.Generic;
using System;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>(); // Dictionary to store item tags and their counts

    // Add an item to the inventory
    public void AddItem(string itemTag, int count)
    {
        if (items.ContainsKey(itemTag))
        {
            items[itemTag] += count;
        }
        else
        {
            items[itemTag] = count;
        }
        Debug.Log("Added " + count + " " + itemTag + " to inventory.");
    }

    // Check if the inventory contains an item
    public bool HasItem(string itemTag)
    {
        return items.ContainsKey(itemTag) && items[itemTag] > 0;
    }

    // Remove an item from the inventory
    public void RemoveItem(string itemTag, int count)
    {
        if (items.ContainsKey(itemTag))
        {
            items[itemTag] -= count;
            if (items[itemTag] < 0)
            {
                items[itemTag] = 0;
            }
            Debug.Log("Removed " + count + " " + itemTag + " from inventory.");
        }
        else
        {
            Debug.Log("Inventory does not contain " + itemTag + ".");
        }
    }

    // Get the count of an item in the inventory
    public int GetItemCount(string itemTag)
    {
        if (items.ContainsKey(itemTag))
        {
            return items[itemTag];
        }
        return 0;
    }

    internal IEnumerable<object> GetItems()
    {
        throw new NotImplementedException();
    }
}
