using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torcia : MonoBehaviour
{
    public GameObject flashlight;

    [SerializeField] private bool on;
    [SerializeField] private bool off;
    [SerializeField] private float flickerTime = 2.5f;
    [SerializeField] private float elapsedTime;

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
    public void Flickering()
    {
        StartCoroutine(FlickeringLight());
    }
    IEnumerator FlickeringLight()
    {   
        elapsedTime=0f;
        while (elapsedTime<flickerTime)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            flashlight.SetActive(!flashlight.activeSelf);
            elapsedTime += Time.deltaTime*10;
        }
    }
}
