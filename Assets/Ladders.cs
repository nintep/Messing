using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladders : MonoBehaviour
{
    private bool playerIsPressingUp = true;
    private bool playerIsColliding = false;

    [SerializeField] float movementSpeed = 2f;

    private PlayerMovement Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsColliding && Player.IsPressingUp)
        {
            transform.position += new Vector3(0, -movementSpeed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsColliding = false;
        }
    }
}
