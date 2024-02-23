using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    public bool ShowPath = false;
    [SerializeField] List<GameObject> path;
    private LineRenderer LineRend;

    private void Awake()
    {
        if (ShowPath)
        {
            LineRend = GetComponent<LineRenderer>();
        }
    }

    void Start()
    {
        if (ShowPath)
        {
            DrawLines();
        }
    }

    private void Update()
    {
        if (ShowPath) DrawLines();
    }

    private void DrawLines()
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (GameObject pathNode in path)
        {
            positions.Add(pathNode.transform.position);
        }

        LineRend.SetPositions(positions.ToArray());
    }
}
