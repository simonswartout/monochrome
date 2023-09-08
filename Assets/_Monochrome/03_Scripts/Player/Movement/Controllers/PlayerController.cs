using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovement", menuName = "ScriptableObjects/PlayerMovementV3")]
public class PlayerController : InputController
{

    public override float RetrieveHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveJumpInput()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
}
