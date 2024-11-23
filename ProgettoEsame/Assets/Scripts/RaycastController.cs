using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private DoorOpen doorScript;
    void Start()
    {
        
    }

    // Update is called once per frame
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

                doorScript = hit.transform.GetComponent<DoorOpen>();
                if (doorScript != null)
                {
                    doorScript.isLocked = false;
                }
            }
            Debug.Log(hit.transform.name);
        }
    }
}
