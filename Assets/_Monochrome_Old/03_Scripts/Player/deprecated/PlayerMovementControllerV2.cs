using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControllerV2 : MonoBehaviour
{
    public enum PlayerState
    {
        Idle, 
        Walking, 
        Running, 
        Jumping,
        Falling
    }

    public bool isGrounded = false;

    private PlayerState _playerState;
    public PlayerState playerState
    { 
        get { return _playerState; }
        set 
        { 
            if(_playerState != value)
            {
                _playerState = value;
                Debug.Log("Player state changed to: " + _playerState);
            }
        }
    }


    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float airSpeed = .5f;

    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private float jumpTime = 0.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float downForce = 500f;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Mathf.Abs(horizontalInput) > 0 && !Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            playerState = PlayerState.Walking;
        }
        else if (Mathf.Abs(horizontalInput) > 0 && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            playerState = PlayerState.Running;

        }
        else if (!isGrounded && rb.velocity.y < 0)
        {
            playerState = PlayerState.Falling;
            AirWalk();
        }
        else
        {
            playerState = PlayerState.Idle;
        }


        if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumps)
        {
            jumpCount++;
            Jump();
            playerState = PlayerState.Jumping;
        }
        //else if (rb.velocity.y == 0 && !isGrounded)
        // {
        //     SetPlayerState(PlayerState.Falling);
        // }   
    }

    void FixedUpdate()
    {
        //rb.AddForce(Vector2.down * downForce * Time.fixedDeltaTime, ForceMode2D.Force);


        isGrounded = IsGrounded();

        if(isGrounded)
        {
            jumpCount = 0;
        }

        switch (_playerState)
        {
            case PlayerState.Idle:
                // Handle idle state
                break;
            case PlayerState.Walking:
                // Handle walking state
                Walk();
                break;
            case PlayerState.Running:
                // Handle running state
                Run();
                break;
            case PlayerState.Jumping:
                // Handle jumping state
                Jump();
                break;
            case PlayerState.Falling:
                // Handle falling state
                Fall();
                break;
            default:
                break;
        }

        //add gravity
    }

    private void Walk()
    {
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.AddForce(new Vector2(horizontalInput * walkSpeed, rb.velocity.y), ForceMode2D.Force);

    }

    private void Run()
    {
        rb.AddForce(new Vector2(horizontalInput * runSpeed, rb.velocity.y), ForceMode2D.Force);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playerState = PlayerState.Falling;    
    }
    

    private void SetDrag(float drag)
    {
        rb.drag = drag;
    }

    private void Fall()
    {
        //SetDrag(1f);
        rb.AddForce(Vector2.down * downForce * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    private void AirWalk()
    {
        rb.AddForce(new Vector2(horizontalInput * airSpeed, 0f), ForceMode2D.Force);
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(GetComponent<Collider2D>().bounds.center, GetComponent<Collider2D>().bounds.size, 0f, Vector2.down, 0.1f, groundLayer);

        return raycastHit2D.collider != null;
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
