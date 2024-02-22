using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class PlayerHealth : MonoBehaviour
{
    public int StartHealth;
    public int CurrentHealth {get; private set;}
    public float HitInvincibilityTime;
    public int DeathYCoordinate = -100;

    private Vector3 startPosition;
    public Checkpoint CurrentCheckpoint {get; private set;}

    public event Action PlayerDied;

    private float invincibilityRemaining = 0;

    private HealthBar healthBar;

    private void Awake()
    {
        startPosition = transform.position;
        CurrentHealth = StartHealth;

        healthBar = FindObjectOfType<HealthBar>();
    }

    private void Start()
    {
        healthBar?.SetHealth(CurrentHealth);
    }

    private void Update()
    {
        if (transform.position.y < DeathYCoordinate)
        {
            RemoveHealth(CurrentHealth);
            return;
        }

        if (invincibilityRemaining >= 0)
        {
            invincibilityRemaining -= Time.deltaTime;
        }
    }

    public void AddHealth(int amount)
    {
        CurrentHealth += amount;
        healthBar?.SetHealth(CurrentHealth);
    }

    public void RemoveHealth(int amount)
    {
        if (invincibilityRemaining > 0)
        {
            return;
        }
        
        invincibilityRemaining = HitInvincibilityTime;

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(0, CurrentHealth);

        healthBar?.SetHealth(CurrentHealth);

        if (CurrentHealth == 0)
        {
            PlayerDied?.Invoke();
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        if (CurrentCheckpoint == null)
        {
            transform.position = startPosition;
        }
        else
        {
            transform.position = CurrentCheckpoint.transform.position;
        }

        ResetHealth();
        invincibilityRemaining = 0;
    }

    private void ResetHealth()
    {
        CurrentHealth = StartHealth;
        healthBar?.SetHealth(CurrentHealth);
    }

    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        CurrentCheckpoint?.DeactivateCheckpoint();
        CurrentCheckpoint = checkpoint;
        checkpoint.ActivateCheckpoint();
    }


}
