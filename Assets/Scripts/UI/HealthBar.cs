using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject HealthIconPrefab;
    public GameObject HealthIconParent;

    private List<GameObject> HealthIcons;

    private void Awake()
    {
        HealthIcons = new List<GameObject>();
    }

    public void SetHealth(int health)
    {
        while (HealthIcons.Count < health)
        {
            AddIcon();
        }

        while (HealthIcons.Count > health)
        {
            RemoveIcon();
        }
    }

    private void AddIcon()
    {
        GameObject newIcon = GameObject.Instantiate(HealthIconPrefab, transform.position, transform.rotation);
        newIcon.transform.SetParent(HealthIconParent.transform, false);

        HealthIcons.Add(newIcon);
    }

    private void RemoveIcon()
    {
        GameObject icon = HealthIcons.Last();
        HealthIcons.Remove(icon);

        Destroy(icon);
    }

}
