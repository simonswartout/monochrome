using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Monochrome.Movement
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] float speed = 1f;
        [SerializeField] float jumpCooldown = 2f;
        [SerializeField] float jumpPower = 2f;
        [SerializeField] float gravity = 9.8f;
        [SerializeField] int numberOfBounces = 1;
        [SerializeField] Vector2 velocity;
        [SerializeField] Transform feet;
        [SerializeField] LayerMask groundLayer;

        public enum MovementState
        {
            MoveRight,
            MoveLeft,
            Jump,
            Idle
        }

        [SerializeField] MovementState movementState;
        public MovementState Movement { get => movementState; set => movementState = value; }

        private void Update()
        {

            Vector2 input = GetInput();
            SetMovementState(input);

            ApplyGravity();
        }

        public Vector2 GetInput()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            return input;
        }

        public MovementState SetMovementState(Vector2 input)
        {
            switch (input)
            {
                case Vector2 _input when input.x > 0:
                    movementState = MovementState.MoveRight;
                    break;
                case Vector2 _input when input.x < 0:
                    movementState = MovementState.MoveLeft;
                    break;
                case Vector2 _input when input.y > 0:
                    movementState = MovementState.Jump;
                    break;
                default:
                    movementState = MovementState.Idle;
                    break;
            }

            Move(movementState);
            SetAnimationState(movementState);
            return movementState;
        }

        private void ApplyGravity()
        {
            if (!IsGrounded())
            {
                //continuously apply gravity to the player
                transform.position = new Vector2(transform.position.x, transform.position.y - gravity * Time.deltaTime);
            }
        }

        private void Move(MovementState movementState)
        {
            
            switch (movementState)
            {
                case MovementState.MoveRight:
                    if(IsGrounded())
                    {
                        transform.DOJump(transform.position + new Vector3(1f, 0f, 0f), jumpPower, numberOfBounces, jumpCooldown);
                    }

                    
                    break;
                case MovementState.MoveLeft:
                    //dojump to the left
                    if(IsGrounded())
                    {
                        transform.DOJump(transform.position + new Vector3(-1f, 0f, 0f), jumpPower, numberOfBounces, jumpCooldown);
                    }
                    
                    break;
                case MovementState.Jump:
                    if (IsGrounded())
                    {
                        transform.DOJump(transform.position, jumpPower * 2, numberOfBounces, jumpCooldown);
                    }
                    break;
                case MovementState.Idle:
                    //not implemented yet
                    break;
            }
        }

        private void SetAnimationState(MovementState movementState)
        {
            switch (movementState)
            {
                case MovementState.MoveRight:
                    animator.Play("MoveRight");
                    break;
                case MovementState.MoveLeft:
                    animator.Play("MoveLeft");
                    break;
                case MovementState.Jump:
                    //not implemented yet
                    break;
                case MovementState.Idle:
                    animator.Play("Idle");
                    break;
            }
        }

        private bool IsGrounded()
        {
            //raycast from the players feet transform
            RaycastHit2D hit = Physics2D.Raycast(feet.position, Vector2.down, 0.1f, groundLayer);
            
            if (hit.collider != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        

    }
}

