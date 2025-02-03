using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinale : MonoBehaviour
{
    public Animator doorAnimator;
    
    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        StartCoroutine(WaitForAnimationStart());
        


    }

    // Update is called once per frame
    
    private IEnumerator WaitForAnimationStart()
    {
        // Wait until startAnimation becomes true
        yield return new WaitUntil(() => CorridoioManager.instance.startAnimationPorta);

        // Execute the code after startAnimation becomes true
        // Place your code here
        doorAnimator.SetBool("DoubleDoorOpen", true);
    }

    
    public void LoadPassato()
    {

       StanzaFinaleManager.instance.LoadStanzaPassato();
    }
}