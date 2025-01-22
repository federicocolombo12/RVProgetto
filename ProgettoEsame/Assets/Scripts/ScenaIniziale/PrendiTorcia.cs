using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrendiTorcia : MonoBehaviour, IInteractable
{
    [SerializeField] PickUpTorcia pickUpScript;
    [SerializeField] Raccolta_Vedi_Oggetto raccoltaScript;
    public bool stopView = false;
    public bool stopPickUp = false;
    void Start()
    {
        pickUpScript = GetComponent<PickUpTorcia>();
        raccoltaScript = GetComponent<Raccolta_Vedi_Oggetto>();
    }
    void Update()
    {
        if (raccoltaScript.rotazione)
        {
            raccoltaScript.RotateObject();
        }
    }

    // Metodo per l'interazione
    public void Interact(GameObject interactor)
    {
        // Trova il componente Raccolta_Vedi_Oggetto sull'interactor

        if (raccoltaScript != null)
        {
            // Avvia la visualizzazione dell'oggetto
            raccoltaScript.StartViewing();
            Debug.Log("Torcia presa");
        }
        else
        {
            Debug.LogWarning("L'interactor non ha il componente Raccolta_Vedi_Oggetto!");
        }
    }

    public void StopInteract(GameObject interactor)
    {
        if (!stopPickUp) { 
        pickUpScript.StartReturnAndDestroy();
            stopPickUp = true;
        }
    }
}

