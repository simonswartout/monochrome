using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] PlayerMoveStateMachine playerMovementStates = null;
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;

    private Rigidbody2D _body;
    private Ground _ground;
    private Vector2 _velocity;

    [SerializeField] private InputController input = null;

    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed;

    private bool _desiredJump, _onGround;


    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();

        _defaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        _desiredJump |= input.RetrieveJumpInput();
        ReturnState();
    }

    private void FixedUpdate()
    {
        _onGround = _ground.GetOnGround();
        _velocity = _body.velocity;

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (_body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (_body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if(_body.velocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale;
        }

        _body.velocity = _velocity;
    }
    private void JumpAction()
    {
        if (_onGround || _jumpPhase < _maxAirJumps)
        {
            _jumpPhase += 1;
            
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
            
            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(_body.velocity.y);
            }
            _velocity.y += _jumpSpeed;
        }
    }

    private PlayerMoveStateMachine ReturnState()
    {
        //set the player movement state based on the jump process
        if (_onGround)
        {
            playerMovementStates.playerJumpState = PlayerMoveStateMachine.PlayerJumpStates.Grounded;
        }
        else if (!_onGround && _velocity.y > 0)
        {
            playerMovementStates.playerJumpState = PlayerMoveStateMachine.PlayerJumpStates.Jumping;
        }
        else if (!_onGround && _velocity.y < 0)
        {
            playerMovementStates.playerJumpState = PlayerMoveStateMachine.PlayerJumpStates.Falling;
        }

        return playerMovementStates;
    }

}
