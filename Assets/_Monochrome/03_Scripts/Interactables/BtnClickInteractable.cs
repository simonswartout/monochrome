using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickInteractable : BaseInteractable
{
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

}
