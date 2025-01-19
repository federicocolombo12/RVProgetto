using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torcia : MonoBehaviour
{
    public GameObject flashlight;

    [SerializeField] private bool on;
    [SerializeField] private bool off;

    void Start()
    {
        on = true;
        off = false;
        flashlight.SetActive(false);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tasto sinistro del mouse premuto");
        }

        if (off && Input.GetMouseButtonDown(0))
        {
            flashlight.SetActive(true);
            off = false;
            on = true;
            Debug.Log("Torcia accesa");
        }
        else if (on && Input.GetMouseButtonDown(0))
        {
            flashlight.SetActive(false);
            off = true;
            on = false;
            Debug.Log("Torcia spenta");
        }
    }
}
