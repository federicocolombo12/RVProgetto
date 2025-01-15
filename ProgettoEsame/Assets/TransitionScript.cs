using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    Animator transitionAnim;
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
