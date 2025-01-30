using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLamp : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void TriggerFall()
    {
        rb.isKinematic = false;
    }
}
