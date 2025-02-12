using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLibrary : MonoBehaviour
{
    [SerializeField] float rotateDuration = 5f; // Durata in secondi della rotazione
    [SerializeField] float rotationAngle = -45f; // Angolo di rotazione sull'asse X
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform targetPosition;
    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void ActivateRb() { 
        rb.isKinematic = false;
    }
    public void setPosition()
    {
        transform.rotation = targetPosition.rotation;
        transform.position = targetPosition.position;
    }
    
}
