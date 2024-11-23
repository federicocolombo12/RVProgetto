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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.gameObject.CompareTag("Door") )
                {
                    
                    doorAnimator = hit.transform.gameObject.GetComponent<Animator>();
                    doorAnimator.SetTrigger("TriggerDoor");
                }
               
               
                
                              
                
            }
            Debug.Log(hit.transform.name);
        }
    }
}
