using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : BtnClickInteractable
{
    public bool isPickedUp = false;
    
    protected override void OnInteract()
    {
        base.OnInteract();
        if (isPickedUp)
        {
            isPickedUp = false;
            transform.parent = null;
            canInteract = true;
            GetComponent<Rigidbody2D>().isKinematic = false;
            PlayerInventoryManager.Instance.RemoveItem(this);
        }
        else
        {
            isPickedUp = true;  
            canInteract = false;
            transform.parent = Game.Instance.transform;
            GetComponent<Rigidbody2D>().isKinematic = true;
            PlayerInventoryManager.Instance.AddItem(this);
        }
    }

}
