using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RaycastController : MonoBehaviour
{
    private Animator doorAnimator;
    
    // Update is called once per frame
    private void Start()
    {
        
    }

    void Update()
    {
        Raycast();
    }
    void Raycast()
    {
        OpenDoor();
        GrabObject();

    }
    void OpenDoor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Door"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    doorAnimator = hit.transform.GetComponent<Animator>();
                    doorAnimator.SetTrigger("TriggerDoor");
                }
            }
            
        }
    }
    void GrabObject()
    { 
       RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
           
            if (hit.transform.CompareTag("Grabbable"))
            {
               if (Input.GetMouseButtonDown(0))
               {
                   hit.transform.SetParent(transform);
                   hit.transform.GetComponent<Rigidbody>().isKinematic = true;
               }
            }
        }
    }
}
