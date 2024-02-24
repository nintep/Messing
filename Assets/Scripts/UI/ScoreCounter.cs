using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int CurrentScore { get; private set; }
    public TMP_Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        CurrentScore = 0;
        ScoreText.text = "Score: " + CurrentScore;
    }

    public void AddScore()
    {
        CurrentScore++;
        ScoreText.text = "Score: " + CurrentScore;

        GetComponent<AudioSource>()?.Play();

        FindObjectOfType<DebugConsole>().AddMessage("Score thing collected", false);
    }
}
