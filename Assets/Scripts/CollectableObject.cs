using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public CollectibleType Type;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (Type == CollectibleType.health)
            {
                IncreasePlayerHealth(collider.gameObject);
            }

            Destroy(gameObject);
        }
    }

    public void IncreasePlayerHealth(GameObject Player)
    {
        Player.GetComponent<PlayerHealth>().AddHealth(1);
    }
}

public enum CollectibleType
{
    health,
    score
}
