using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] PlayerMoveStateMachine playerMovementStates = null;
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0, 100f)] private float maxAcceleration = 4f;
    [SerializeField, Range(0, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Ground ground;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveHorizontalInput();
        Debug.Log(direction.x);
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);
        ReturnMovementState();
        ReturnDirectionState();
    }

    private void FixedUpdate() 
    {
        velocity = rb.velocity;
        onGround = ground.GetOnGround();

        maxSpeedChange = maxAcceleration * Time.deltaTime;
        acceleration = onGround ? maxAcceleration : maxAirAcceleration;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        rb.velocity = velocity;
    }

    private PlayerMoveStateMachine ReturnMovementState()
    {
        if (direction.x == 0)
        {
            playerMovementStates.playerMovementState = PlayerMoveStateMachine.PlayerMovementStates.Idle;
        }
        else if (direction.x != 0 && onGround)
        {
            playerMovementStates.playerMovementState = PlayerMoveStateMachine.PlayerMovementStates.Walking;
        }
        else if (direction.x != 0 && !onGround)
        {
            playerMovementStates.playerMovementState = PlayerMoveStateMachine.PlayerMovementStates.AirWalk;
        }
        else
        {
            playerMovementStates.playerMovementState = PlayerMoveStateMachine.PlayerMovementStates.Idle;
        }

        return playerMovementStates;
    }

    private PlayerMoveStateMachine ReturnDirectionState()
    {
        if (direction.x < 0)
        {
            playerMovementStates.playerDirectionState = PlayerMoveStateMachine.PlayerDirectionStates.Left;
        }
        else if (direction.x > 0)
        {
            playerMovementStates.playerDirectionState = PlayerMoveStateMachine.PlayerDirectionStates.Right;
        }
        else
        {
            playerMovementStates.playerDirectionState = PlayerMoveStateMachine.PlayerDirectionStates.Right;
        }

        return playerMovementStates;
    }
}
