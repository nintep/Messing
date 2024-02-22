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

    private bool jumpAvailable = true;
    public float maxHorizontalVelocity = 16;

    private Vector3 externalForce = new Vector3(0, 0, 0);
    private bool affectedByExternalForce = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x * moveSpeed;
        affectedByExternalForce = false; //pressing movement keys cancels external force
    }

    private void OnJump(InputValue movementValue)
    {
        if (jumpAvailable)
        {
            movementY = jumpForce;
        }
    }
    
    private void FixedUpdate()
    {
        if(movementX == 0 && !affectedByExternalForce)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (Mathf.Abs(rb.velocity.x) < maxHorizontalVelocity)
        {
            rb.AddForce(new Vector2(movementX, 0));
        }

        if (jumpAvailable && movementY != 0)
        {
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
        externalForce = force;
        affectedByExternalForce = true;
    }
}
