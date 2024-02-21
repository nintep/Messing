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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x * moveSpeed;
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
        if(movementX == 0)
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!jumpAvailable)
        {
            Debug.Log("Jump refreshed");
            jumpAvailable = true;
        }
        
    }
}
