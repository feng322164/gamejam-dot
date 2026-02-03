using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item it)
    {
        if (it == null) return;
        items.Add(it);
    }

    public bool RemoveItem(Item it)
    {
        return items.Remove(it);
    }

    public bool HasItem(Item it)
    {
        return items.Contains(it);
    }
}
