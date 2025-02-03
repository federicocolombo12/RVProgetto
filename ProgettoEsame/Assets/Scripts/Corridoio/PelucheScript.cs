using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelucheScript : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void TriggerPeluche()
    {
        anim.SetTrigger("TriggerPeluche");
    }
}
