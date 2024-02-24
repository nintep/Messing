using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationController : MonoBehaviour
{
    public bool UseVerticalAnimation = false;
    
    public string HorizontalStateName;
    public string VerticalStateName;

    public Animator Animator;

    private void Start()
    {
        Animator.Play(UseVerticalAnimation ? VerticalStateName : HorizontalStateName);
    }
}
