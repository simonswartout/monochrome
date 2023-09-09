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
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            isPickedUp = true;
            transform.parent = Game.Instance.transform;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

}
