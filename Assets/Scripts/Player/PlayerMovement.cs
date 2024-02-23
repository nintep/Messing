using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5;
    public float jumpForce = 1;
    public float hitInputCooldown = 0.5f;
    public float maxHorizontalVelocity = 16;

    public bool jumpAvailable = true;

    private bool inputBlocked = false;
    private float inputBlockedCooldown;

    private float inputMovementX;
    private float inputMovementY;
    private bool inputJump;

    private Vector3 externalForce = new Vector3(0, 0, 0);
    private bool affectedByExternalForce = false;

    private bool removeVelocity = false;

    public bool IsPressingUp {get; private set;}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue movementValue)
    {
        if (inputBlocked)
        {
            inputMovementX = 0;
            inputMovementY = 0;
        }
        else
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            inputMovementX = movementVector.x;
            inputMovementY = movementVector.y;

            IsPressingUp = movementVector.y > 0;
        }
    }

    private void OnJump()
    {
        if (inputBlocked || !jumpAvailable)
        {
            inputJump = false;
        }
        else
        {
            inputJump = true;
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!jumpAvailable)
        {
            //Debug.Log("Jump refreshed");
            jumpAvailable = true;
        }
    }

    public void SetExternalForce(Vector3 force)
    {
        Debug.Log("Add external force");
        inputBlocked = true;
        inputBlockedCooldown = hitInputCooldown;

        externalForce = force;
        affectedByExternalForce = true;
    }

    public void RemoveVelocity()
    {
        removeVelocity = true;
    }

    private void Update()
    {
        if (inputBlockedCooldown > 0)
        {
            inputBlockedCooldown -= Time.deltaTime;
            if (inputBlockedCooldown <= 0)
            {
                inputBlocked = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Add movement from input
        if (!inputBlocked)
        {
            rb.velocity = new Vector2(inputMovementX * moveSpeed, rb.velocity.y);
            //nputMovementX = 0;
            //inputMovementY = 0;
        }

        if (jumpAvailable && inputJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            inputJump = false;
            jumpAvailable = false;
        }

        //Add movement from external forces
        if (affectedByExternalForce)
        {
            rb.AddForce(Vector2.up * externalForce.y, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x + externalForce.x, rb.velocity.y);

            externalForce = Vector2.zero;
        }

        //check if velocity is too high
        if (Mathf.Abs(rb.velocity.x) > maxHorizontalVelocity)
        {
            rb.velocity = new Vector2(maxHorizontalVelocity, rb.velocity.y);
        }

        if (removeVelocity)
        {
            rb.velocity = Vector2.zero;
            removeVelocity = false;
        }
    }
}
