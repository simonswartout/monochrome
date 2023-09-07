using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Falling
    }

    [SerializeField] private PlayerState currentState;
    [SerializeField] private PlayerState previousState;
    public bool isGrounded = false;


    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int jumpCount = 0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float downForce = 500f;

    private float horizontalInput = 0f;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get user input
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
        }

        //update the player state
        UpdatePlayerState();
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded in FixedUpdate
        isGrounded = IsGrounded();

        // Apply behavior based on the current state
        switch (currentState)
        {
            case PlayerState.Idle:
                // Handle idle state
                break;

            case PlayerState.Walking:
                // Handle walking state
                Move(horizontalInput, isJumping);
                break;

            case PlayerState.Running:
                // Handle running state
                Move(horizontalInput, isJumping);
                break;

            case PlayerState.Jumping:
                // Handle jumping state
                Jump();
                break;

            case PlayerState.Falling:
                // Handle falling state
                Falling();
                break;
        }
    }

    private void UpdatePlayerState()
    {
        previousState = currentState;

        if (isJumping)
        {
            ChangeState(PlayerState.Jumping);
        }
        else if (Mathf.Abs(horizontalInput) > 0 && isGrounded)
        {
            ChangeState(Input.GetKey(KeyCode.LeftShift) ? PlayerState.Running : PlayerState.Walking);
        }
        else if (Mathf.Abs(horizontalInput) == 0 && currentState != PlayerState.Falling)
        {
            ChangeState(PlayerState.Idle);
        }
    }

    private void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log("Player state changed to: " + currentState);
        }
    }

    public PlayerState GetPlayerState()
    {
        return currentState;
    }

    private void Move(float horizontalInput, bool jump)
    {
        Vector2 moveDirection = new Vector2(horizontalInput, 0);

        rb.velocity = new Vector2(moveDirection.x * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed), rb.velocity.y);
    }

    private void AirMove(Vector2 direction)
    {
        rb.AddForce(direction * 5f, ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        // Cast a ray slightly below the character's feet to check for ground
        Vector2 rayStart = transform.position + Vector3.down * 1.1f; // Adjust the value to match your character's size
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 1.1f, groundLayer);

        // Ignore permeable platforms
        // if (hit.collider != null && hit.collider.CompareTag("Permeable_Platform"))
        // {
        //     hit = Physics2D.Raycast(rayStart, Vector2.down, 0.1f, groundLayer);
        // }

        if(hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void Jump()
    {
        // Check if the player can jump (based on maxJumps)
        if (jumpCount < maxJumps)
        {
            // Check if the player is grounded to reset jump count
            if (isGrounded)
            {
                jumpCount = 0;
            }

            // Apply the jump force
            rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            jumpCount++;
            isJumping = false;
            ChangeState(PlayerState.Falling);
        }
    }

    private void Falling()
    {
        // Check if the player is no longer jumping (has reached the peak of the jump)
        if (rb.velocity.y < 0)
        {
            // Apply gravity
            rb.AddForce(Vector2.down * downForce * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        // Apply horizontal air movement
        AirMove(new Vector2(horizontalInput, 0));
    }

    public float GetVelocityX()
    {
        return rb.velocity.x;
    }

    public float GetVelocityY()
    {
        return rb.velocity.y;
    }
}
