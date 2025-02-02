using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLibrary : MonoBehaviour
{
    [SerializeField] float rotateDuration = 5f; // Durata in secondi della rotazione
    [SerializeField] float rotationAngle = -45f; // Angolo di rotazione sull'asse X
    [SerializeField] Rigidbody rb;
    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void ActivateRb() { 
        rb.isKinematic = false;
    }

    
}
