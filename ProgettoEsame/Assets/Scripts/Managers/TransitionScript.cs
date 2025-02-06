using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static TransitionScript instance { get; private set; }
    Animator[] transitionAnim;

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
        transitionAnim = GetComponentsInChildren<Animator>();
    }
    public void FadeIn(int Index)
    {
        transitionAnim[Index].SetTrigger("FadeIn");
    }

    public void FadeOut(int Index)
    {
        transitionAnim[Index].SetTrigger("FadeOut");
    }
    public bool IsFadingOut(int Index)
    {
        // Restituisce true se l'animazione FadeOut è attiva
        return transitionAnim[Index].GetCurrentAnimatorStateInfo(0).IsName("FadeOut");
    }
}
