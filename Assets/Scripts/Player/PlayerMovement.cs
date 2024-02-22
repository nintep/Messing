using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float movementX;
    private float movementY;
    public float moveSpeed = 5;
    public float jumpForce = 1;
    public float hitInputCooldown = 0.5f;

    private bool jumpAvailable = true;
    public float maxHorizontalVelocity = 16;

    private Vector3 externalForce = new Vector3(0, 0, 0);
    private bool affectedByExternalForce = false;

    private bool StopHorizontalMovement = false;
    private bool inputBlocked = false;
    private float inputCooldownRemaining = 0;

    public bool IsPressingUp {get; private set;}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnMove(InputValue movementValue)
    {
        if (inputBlocked)
        {
            return;
        }

        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x * moveSpeed;
        IsPressingUp = movementVector.y > 0;
        affectedByExternalForce = false; //pressing movement keys cancels external force
    }

    private void OnMoveReleased()
    {
        if (inputBlocked)
        {
            return;
        }

        StopHorizontalMovement = true;
    }

    private void OnJump()
    {
        if (inputBlocked)
        {
            return;
        }

        if (jumpAvailable)
        {
            movementY = jumpForce;
        }
    }
    
    private void FixedUpdate()
    {
        inputCooldownRemaining -= Time.fixedDeltaTime;
        if (inputCooldownRemaining <= 0)
        {
            inputBlocked = false;
        }

        if (StopHorizontalMovement)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            StopHorizontalMovement = false;
        }
        else if (Mathf.Abs(rb.velocity.x) < maxHorizontalVelocity)
        {
            rb.AddForce(new Vector2(movementX, 0));
        }

        if (jumpAvailable && movementY != 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            
            rb.AddForce(Vector2.up * movementY, ForceMode2D.Impulse);
            movementY = 0;
            jumpAvailable = false;
        }

        if (affectedByExternalForce)
        {
            //After the force is applied, reset the external force but don't reset the affectedByExternalForce flag until player presses movement keys
            rb.AddForce(externalForce, ForceMode2D.Impulse);
            externalForce = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!jumpAvailable)
        {
            Debug.Log("Jump refreshed");
            jumpAvailable = true;
        }
    }

    public void SetExternalForce(Vector3 force)
    {
        inputBlocked = true;
        inputCooldownRemaining = hitInputCooldown;
        movementX = 0;
        externalForce = force;
        affectedByExternalForce = true;
    }
}
