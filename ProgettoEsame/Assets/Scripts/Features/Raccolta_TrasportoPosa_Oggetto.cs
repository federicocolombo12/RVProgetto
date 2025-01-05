using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccolta_TrasportoPosa_Oggetto : MonoBehaviour
{
    private GameObject pickedObject = null;
    public Transform holdPosition;
    public LayerMask interactableLayer; // LayerMask per gli oggetti interagibili

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pickedObject == null)
            {
                // Prova a raccogliere un oggetto
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.0f, interactableLayer))
                {
                    Debug.Log("Raycast hit: " + hit.transform.name);
                    if (hit.transform.GetComponent<Rigidbody>())
                    {
                        pickedObject = hit.transform.gameObject;
                        pickedObject.GetComponent<Rigidbody>().isKinematic = true;
                        pickedObject.transform.position = holdPosition.position;
                        pickedObject.transform.parent = holdPosition;
                        Debug.Log("Picked up: " + pickedObject.name);
                    }
                }
                else
                {
                    Debug.Log("Raycast did not hit any object");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (pickedObject != null)
            {
                // Rilascia l'oggetto
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.transform.parent = null;
                Debug.Log("Dropped: " + pickedObject.name);
                pickedObject = null;
            }
        }
    }
}