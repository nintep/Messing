using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] float speed = 2f;

    private int nextPathNodeIndex = 0;
    private Transform nextPathNode;

    // Start is called before the first frame update
    void Start()
    {
        nextPathNode = path.transform.GetChild(nextPathNodeIndex);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPathNode.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPathNode.position) < 0.1f)
        {
            nextPathNodeIndex = (nextPathNodeIndex + 1) % path.transform.childCount;
            nextPathNode = path.transform.GetChild(nextPathNodeIndex);
        }
    }
}
