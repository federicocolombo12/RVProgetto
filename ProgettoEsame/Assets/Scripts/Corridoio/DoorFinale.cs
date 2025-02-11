using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinale : MonoBehaviour
{
    public Animator doorAnimator;
    private DoorFinaleAudio doorFinaleAudio;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorFinaleAudio = GetComponent<DoorFinaleAudio>();
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
        doorFinaleAudio?.PlayDoorOpenSound();
    }

    
    public void LoadPassato()
    {

       StanzaFinaleManager.instance.LoadStanzaPassato();
    }
    public void ChiudiPorta()
    {
        doorAnimator.SetBool("DoubleDoorOpen", false);
        doorFinaleAudio?.PlayDoorCloseSound();
    }
}