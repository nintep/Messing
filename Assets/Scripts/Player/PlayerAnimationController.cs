using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public string idleAnimation;
    public string runAnimation;
    public string jumpAnimation;
    public string climbAnimation;

    private PlayerAnimationState currentState;
    private Rigidbody2D rb;
    private PlayerMovement movement;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Vector2 velocity = rb.velocity;

        if (movement.IsPressingUp)
        {
            PlayAnimation(PlayerAnimationState.climb);
        }
        else if (movement.IsJumping)
        {
            PlayAnimation(PlayerAnimationState.jump);
        }
        else if (Mathf.Abs(velocity.x) > 0)
        {
            PlayAnimation(PlayerAnimationState.run);
        }
        else
        {
            PlayAnimation(PlayerAnimationState.idle);
        }
    }

    private void PlayAnimation(PlayerAnimationState state)
    {
        if (currentState != state)
        {
            string stateName;

            switch (state)
            {
                case PlayerAnimationState.idle:
                    stateName = idleAnimation;
                    break;
                case PlayerAnimationState.run:
                    stateName = runAnimation;
                    break;
                case PlayerAnimationState.jump:
                    stateName = jumpAnimation;
                    break;
                case PlayerAnimationState.climb:
                    stateName = climbAnimation;
                    break;
                default:
                    stateName = idleAnimation;
                    break;
            }

            animator.Play(stateName);
            currentState = state;
        }
    }


}

public enum PlayerAnimationState
{
    idle,
    run,
    jump,
    climb
}
