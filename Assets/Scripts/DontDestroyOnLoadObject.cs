using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
