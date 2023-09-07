using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovementControllerV2 playerMovementController;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        if(playerMovementController != null)
        {
            SetWalkRunAnimation();
        }
    }

    private void SetWalkRunAnimation()
    {
        FlipSpriteRenderer();

        if(playerMovementController.GetPlayerState() == PlayerMovementControllerV2.PlayerState.Walking && playerMovementController.isGrounded == true)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if(playerMovementController.GetPlayerState() == PlayerMovementControllerV2.PlayerState.Running && playerMovementController.isGrounded == true)
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
        float XVelocity = playerMovementController.GetVelocityX();

        if(XVelocity > 0.5)
        {
            spriteRenderer.flipX = false;
        }
        else if(XVelocity < -0.5)
        {
            spriteRenderer.flipX = true;
        }
        else if(XVelocity == 0)
        {
            SetIdleAnimation();
        }
    }

    private void SetIdleAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

   
}
