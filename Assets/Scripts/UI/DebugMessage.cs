using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMessage : MonoBehaviour
{
    public TMP_Text Text;
    public float LifeSpan = 4f;
    private float lifeLeft;

    public bool isLatestMessage = false;

    private void Awake()
    {
        lifeLeft = LifeSpan;
    }

    private void Update()
    {
        if (lifeLeft > 0)
        {
            lifeLeft -= Time.deltaTime;
        }

        if (lifeLeft <= 0)
        {
            if(!isLatestMessage)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetText(string text)
    {
        Text.text = text;
    }
}
