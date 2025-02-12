using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPazzo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PazzoScript pazzoScript;
    private float lastActionTime = -5f; // Inizializza a -5 per permettere la prima esecuzione immediata
    private const float actionCooldown = 10f; // Intervallo di 5 secondi
    private AudioSource audioSource;
    private bool played = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastActionTime >= actionCooldown)
        {
            pazzoScript.Silence();
            if (!played)
            {                 audioSource.Play();
                played = true;
            }
            
            lastActionTime = Time.time;
        }
    }
}
