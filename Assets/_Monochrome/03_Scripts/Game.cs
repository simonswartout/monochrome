using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField] private PlayerMovementControllerV2 playerMovementController;
    
    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerMovementController.transform.position;
    }
}
