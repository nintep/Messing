using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public CollectibleType Type;

    public bool ResetOnPlayerDeath = true;
    public bool ResetOnCheckpointActivated = true;

    private SpriteRenderer rend;
    private BoxCollider2D boxCollider;

    private PlayerHealth player;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        player = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        if (ResetOnCheckpointActivated)
        {
            player.CheckpointActivated += Activate;
        }

        if (ResetOnPlayerDeath)
        {
            player.PlayerDied += Activate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (Type == CollectibleType.health)
            {
                IncreasePlayerHealth(collider.gameObject);
            }

            if (Type == CollectibleType.score)
            {
                IncreasePlayerScore();
            }

            Deactivate();
        }
    }

    public void IncreasePlayerHealth(GameObject Player)
    {
        Player.GetComponent<PlayerHealth>().AddHealth(1);
    }

    public void IncreasePlayerScore()
    {
        FindObjectOfType<ScoreCounter>().AddScore();
    }

    private void Activate()
    {
        rend.enabled = true;
        boxCollider.enabled = true;
    }

    private void Deactivate()
    {
        rend.enabled = false;
        boxCollider.enabled = false;
    }


}

public enum CollectibleType
{
    health,
    score
}
