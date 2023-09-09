using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField] private PlayerMoveStateMachine playerMovement;
    
    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerMovement.transform.position;
    }

    public PlayerMoveStateMachine.PlayerMovementStates GetPlayerMovementState()
    {
        return playerMovement.playerMovementState;
    }
}
