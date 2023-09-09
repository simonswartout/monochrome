using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager Instance { get; private set; }

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public UnityAction OnInventoryChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(InventoryItem item)
    {
        inventoryItems.Add(item);
        InventoryChanged();
    }

    public void RemoveItem(InventoryItem item)
    {
        inventoryItems.Remove(item);
        InventoryChanged();
    }

    public void InventoryChanged()
    {
        Debug.Log("Inventory changed");
        OnInventoryChanged.Invoke();
    }

    public bool HasItem(InventoryItem item)
    {
        return inventoryItems.Contains(item);
    }

}
