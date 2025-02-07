using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPazzo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PazzoScript pazzoScript;
    private float lastActionTime = -5f; // Inizializza a -5 per permettere la prima esecuzione immediata
    private const float actionCooldown = 5f; // Intervallo di 5 secondi

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastActionTime >= actionCooldown)
        {
            pazzoScript.Silence();
            lastActionTime = Time.time;
        }
    }
}
