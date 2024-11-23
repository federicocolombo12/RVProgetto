using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool isLocked = true;
    public GameObject door;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
    }
    void OpenDoor()
    {
        if (!isLocked) {
           
                door.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
            
            
            isLocked = true;
        }
        
    }
}
