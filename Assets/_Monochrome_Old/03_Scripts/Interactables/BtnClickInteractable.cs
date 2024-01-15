using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickInteractable : BaseInteractable
{
    private bool isActivated = false;
    public bool IsActivated => isActivated;

    protected override void Start()
    {
        base.Start();
        isActivated = false;
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            isActivated = true;
            OnInteract();
        }
    }

}
