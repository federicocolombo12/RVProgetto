using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool Trigger = false;
    public bool Open = false;
    public GameObject door;
    public float doorRotation;
    
   

    // Update is called once per frame
    void Update()
    {
        doorRotation = door.transform.rotation.y*Mathf.Rad2Deg;
        OpenDoor(doorRotation);
    }
    void OpenDoor(float doorRotation)
    {
        
        if (Trigger)
        {
            if (!Open)
            {
                door.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
                Debug.Log("Door is opening");
                if (doorRotation < 40f)
                {
                    
                    Trigger = false;
                    Open = true;
                    Debug.Log("Door is open");
                }
            }
            else
            {
                door.transform.Rotate(Vector3.up, -90f * Time.deltaTime);
                Debug.Log("Door is closing");
                if (doorRotation > 57.29578)
                {
                    Trigger = false;
                    Open = false;
                    Debug.Log("Door is closed");
                }
            }
            

            
        }
        
    }
}
