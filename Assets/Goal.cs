using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] string nextLevel;

    public bool UseFinnishFlagSprite = false;

    private void Awake()
    {
        if (UseFinnishFlagSprite)
        {
            GetComponent<Animator>()?.Play("Flag_Finnish");
        }
        else
        {
            GetComponent<Animator>()?.Play("Flag_Wave");
        }
    }

    //if player collides with goal, go to level with certain name
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

}
