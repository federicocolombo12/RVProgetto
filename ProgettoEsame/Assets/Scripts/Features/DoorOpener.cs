using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Animator doorAnimator;
    
    

    // Update is called once per frame
    void Update()
    {
        if (FirstSceneManager.instance.doorOpenable)
        {
            Debug.Log("Door is now openable!");
            if (Input.GetKeyDown(KeyCode.E))
            {
                FirstSceneManager.instance.doorOpen = true;
                // Wait until Scene is loaded
                
                doorAnimator.SetBool("DoorOpen", true);
                
            }
        }
    }
}
