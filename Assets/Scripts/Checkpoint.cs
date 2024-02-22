using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    private PlayerHealth health;

    public Color ActiveColor;
    public Color InactiveColor;

    public SpriteRenderer sprite;

    private void Awake()
    {
        health = FindAnyObjectByType<PlayerHealth>();
        sprite.color = InactiveColor;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            health.SetActiveCheckpoint(this);
        }
    }

    public void ActivateCheckpoint()
    {
        sprite.color = ActiveColor;
    }

    public void DeactivateCheckpoint()
    {
        sprite.color = InactiveColor;
    }
}
