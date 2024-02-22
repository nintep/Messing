using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingObject : MonoBehaviour
{
    [SerializeField] GameObject bounceCenter;
    [SerializeField] float knockbackForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 force = new Vector3(0, 0, 0);
            Vector3 bounceDirection = collision.gameObject.transform.position - bounceCenter.transform.position;
            
            if (bounceDirection.x >= 0.2f) force += new Vector3(1, 0, 0);
            else if (bounceDirection.x <= -0.2f) force += new Vector3(-1, 0, 0);

            if (bounceDirection.y >= 0.2f) force += new Vector3(0, 1, 0);
            else if (bounceDirection.y <= -0.2f) force += new Vector3(0, -1, 0);
            else force += new Vector3(0, 0.5f, 0);

            collision.gameObject.GetComponent<PlayerMovement>().SetExternalForce(force.normalized * knockbackForce);

            Debug.Log("BOUNCE: " + force.normalized * knockbackForce);

            //TODO player loses health
        }
    }
}
