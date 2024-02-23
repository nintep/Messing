using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float StartTime;
    private float currentTime;

    public TMP_Text TimerText;

    private bool zeroReached = false;

    private void Awake()
    {
        currentTime = StartTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime >= 0)
        {
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            if (!zeroReached)
            {
                zeroReached = true;
                FindObjectOfType<DebugConsole>().AddMessage("timer went to 0", false);
            }

            float minutes = Mathf.FloorToInt(-1.0f * currentTime / 60);
            float seconds = Mathf.FloorToInt(-1.0f * currentTime % 60);

            TimerText.text = string.Format("-{0:00}:{1:00}", minutes, seconds);
        }

        
    }
}
