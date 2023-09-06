using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int jumpCount = 0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded = false;

    private float horizontalInput = 0f;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded in FixedUpdate
        isGrounded = IsGrounded();

        // Apply movement based on user input
        Move(horizontalInput, isJumping);
        isJumping = false; // Reset jump flag after jumping
    }

    private void Move(float horizontalInput, bool jump)
    {
        Vector2 moveDirection = new Vector2(horizontalInput, 0);

        // Apply movement force
        if (!isGrounded)
        {
            AirMove(moveDirection);
        }
        else
        {
            // Use rb.velocity for more precise control
            rb.velocity = new Vector2(moveDirection.x * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed), rb.velocity.y);
        }

        // Handle jumping
        if (jump)
        {
            Jump();
        }
    }

    private void AirMove(Vector2 direction)
    {
        rb.AddForce(direction * walkSpeed * 0.3f, ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        // Do a raycast to check if the player is grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }

    private void Jump()
    {
        // Check if the player is grounded
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Jump
        if (jumpCount < maxJumps)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }
}
