using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static TransitionScript instance { get; private set; }
    Animator transitionAnim;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    void Start()
    {
        transitionAnim = GetComponentInChildren<Animator>();
    }
    public void FadeIn()
    {
        transitionAnim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        transitionAnim.SetTrigger("FadeOut");
    }
}
