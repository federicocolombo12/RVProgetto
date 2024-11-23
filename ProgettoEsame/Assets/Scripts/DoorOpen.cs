using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool Trigger = false;
    public bool Open = false;
    
    private Animator doorAnimatorController;
    public GameObject door;

    private void Start()
    {
        doorAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
        OpenDoor();
    }
    void OpenDoor()
    {
        
        if (Trigger)
        {
            
            
            doorAnimatorController.SetTrigger("TriggerDoor");
            Trigger = false;
            
            

            
        }
        
    }
}
