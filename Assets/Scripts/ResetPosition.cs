using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public bool ResetPositionOnCheckpointActivated = true;
    public bool ResetPositionOnPlayerDeath = true;

    private PlayerHealth playerHealth;
    private Vector2 startPosition;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        startPosition = transform.position;        
    }

    private void Start()
    {
        if (ResetPositionOnCheckpointActivated)
        {
            playerHealth.CheckpointActivated += Reset;
        }

        if (ResetPositionOnPlayerDeath)
        {
            playerHealth.PlayerDied += Reset;
        }
    }

    private void Reset()
    {
        transform.position = startPosition;
    }
}
