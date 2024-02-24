using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    private PlayerHealth health;

    public string ActiveState;
    public string InactiveState;

    public SpriteRenderer sprite;

    public Animator animator;

    private void Awake()
    {
        health = FindAnyObjectByType<PlayerHealth>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        animator.Play(InactiveState);
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
        Debug.Log("activated checkpoint");
        animator.Play(ActiveState);
    }

    public void DeactivateCheckpoint()
    {
        animator.Play(InactiveState);
    }
}
