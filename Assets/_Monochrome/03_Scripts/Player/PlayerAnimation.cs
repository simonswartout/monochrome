using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMoveStateMachine playerMoveStateMachine;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        if(playerMoveStateMachine != null)
        {
            SetWalkRunAnimation();
        }
    }

    private void SetWalkRunAnimation()
    {
        FlipSpriteRenderer();

        if(playerMoveStateMachine.playerMovementState == PlayerMoveStateMachine.PlayerMovementStates.Idle)
        {
            SetIdleAnimation();
        }
        else if(playerMoveStateMachine.playerMovementState == PlayerMoveStateMachine.PlayerMovementStates.Walking)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if(playerMoveStateMachine.playerMovementState == PlayerMoveStateMachine.PlayerMovementStates.Running)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            SetIdleAnimation();
        }
    }

    private void FlipSpriteRenderer()
    {
        if(playerMoveStateMachine.playerDirectionState == PlayerMoveStateMachine.PlayerDirectionStates.Left)
        {
            spriteRenderer.flipX = true;
        }
        else if(playerMoveStateMachine.playerDirectionState == PlayerMoveStateMachine.PlayerDirectionStates.Right)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void SetIdleAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

   
}
