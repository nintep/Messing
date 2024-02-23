using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissplacedCollider : MonoBehaviour
{
    [SerializeField] Vector3 colliderOffset;
    [SerializeField] float resetTimer;

    private Vector3 startLocation;
    private IEnumerator coroutine;

    void Start()
    {
        startLocation = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = startLocation + new Vector3(transform.right.x*colliderOffset.x, transform.right.y*colliderOffset.y, transform.right.z*colliderOffset.z);
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = ResetPosition();
            StartCoroutine(coroutine);
        }
    }

    //coroutine to reset position after a certain time
    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(resetTimer);
        transform.position = startLocation;
    }

}