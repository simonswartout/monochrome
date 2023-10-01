using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovementControllerV4 playerMoveStateMachine;
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
        if(playerMoveStateMachine.PlayerMovementState == PlayerMovementControllerV4.PlayerMovementStates.Walking)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            SetIdleAnimation();
        }
    }

    private void FlipSpriteRenderer()
    {
        if(playerMoveStateMachine.PlayerDirectionState == PlayerMovementControllerV4.PlayerDirectionStates.Left)
        {
            spriteRenderer.flipX = true;
        }
        else if(playerMoveStateMachine.PlayerDirectionState == PlayerMovementControllerV4.PlayerDirectionStates.Right)
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
