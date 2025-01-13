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
        

        if (Input.GetButtonDown("F"))
        {
            Debug.Log("Tasto F premuto");
        }

        if (off && Input.GetButtonDown("F"))
        {
            flashlight.SetActive(true);
            off = false;
            on = true;
            Debug.Log("Torcia accesa");
        }
        else if (on && Input.GetButtonDown("F"))
        {
            flashlight.SetActive(false);
            off = true;
            on = false;
            Debug.Log("Torcia spenta");
        }
    }
}
