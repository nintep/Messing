using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject HealthIconPrefab;
    public GameObject HealthIconParent;

    public AudioClip HealthCollectSound;
    public AudioClip HealthLoseSound;

    private AudioSource audioSource;

    private List<GameObject> HealthIcons;

    private void Awake()
    {
        HealthIcons = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetHealth(int health, bool playSound)
    {
        while (HealthIcons.Count < health)
        {
            AddIcon();
            if (audioSource != null && playSound)
            {
                audioSource.clip = HealthCollectSound;
                audioSource.Play();
            }
        }

        while (HealthIcons.Count > health)
        {
            RemoveIcon();
            if (audioSource != null && playSound)
        {
            audioSource.clip = HealthLoseSound;
            audioSource.Play();
        }
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
