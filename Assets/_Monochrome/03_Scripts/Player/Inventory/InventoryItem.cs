using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public SpriteRenderer itemSprite;
    public string itemDescription;

    [SerializeField] private PickupInteractable pickupInteractable;

    protected virtual void Start()
    {
        if(TryGetComponent(out PickupInteractable pickupInteractable))
        {
            pickupInteractable = GetComponent<PickupInteractable>();
        }
    }

    protected virtual void Update()
    {
        if (pickupInteractable.isPickedUp && !PlayerInventoryManager.Instance.HasItem(this)) //is not in the inventory yet
        {
            PlayerInventoryManager.Instance.AddItem(this);
        }
        else if (!pickupInteractable.isPickedUp && PlayerInventoryManager.Instance.HasItem(this)) //is in the inventory but not picked up
        {
            PlayerInventoryManager.Instance.RemoveItem(this);
        }
    }

    public virtual void OnUse()
    {
        Debug.Log("Using " + itemName);
    }

    public virtual void OnDrop()
    {
        Debug.Log("Dropping " + itemName);
        
    }
}
