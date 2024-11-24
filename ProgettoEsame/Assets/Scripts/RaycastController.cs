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

    }
    void OpenDoor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            Debug.Log(hit.transform.name);
        }
    }
    
}
