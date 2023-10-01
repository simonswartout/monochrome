using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField] private PlayerMovementControllerV4 playerMovement;
    
    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerMovement.transform.position;
    }

    public PlayerMovementControllerV4.PlayerMovementStates GetPlayerMovementState()
    {
        return playerMovement.PlayerMovementState;
    }
}
