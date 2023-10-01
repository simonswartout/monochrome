using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControllerV4 : MonoBehaviour
{
    public enum PlayerMovementStates
    {
        Idle, 
        Walking,
        Running, 
        Jumping
    }

    public enum PlayerDirectionStates
    {
        Left,
        Right
    }

    [SerializeField] private PlayerDirectionStates playerDirectionState;
    [SerializeField] private PlayerMovementStates playerMovementState;

    public PlayerDirectionStates PlayerDirectionState { get => playerDirectionState; set => playerDirectionState = value; }
    public PlayerMovementStates PlayerMovementState { get => playerMovementState; set => playerMovementState = value; }

    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float groundedThreshold = 0.05f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        UpdateMovementState();
        UpdateDirectionState();
    }

    private void UpdateMovementState()
    {
        if (horizontal == 0f)
        {
            playerMovementState = PlayerMovementStates.Idle;
        }
        else
        {
            playerMovementState = PlayerMovementStates.Walking;
        }
        
        if (!IsGrounded())
        {
            playerMovementState = PlayerMovementStates.Jumping;
        }
    }

    private void UpdateDirectionState()
    {
        if (horizontal < 0f)
        {
            playerDirectionState = PlayerDirectionStates.Left;
        }
        else if (horizontal > 0f)
        {
            playerDirectionState = PlayerDirectionStates.Right;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundedThreshold, groundLayer);
    }
}
