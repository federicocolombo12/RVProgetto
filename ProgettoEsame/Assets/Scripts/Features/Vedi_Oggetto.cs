using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vedi_Oggetto : MonoBehaviour, IInteractable
{
    [SerializeField] Raccolta_Vedi_Oggetto raccoltaScript;
    public bool stopView = false;
    void Start() {
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
            
        }
        else
        {
            Debug.LogWarning("L'interactor non ha il componente Raccolta_Vedi_Oggetto!");
        }
    }
    public void StopInteract(GameObject interactor)
    {
        // Trova il componente Raccolta_Vedi_Oggetto sull'interactor
        if (raccoltaScript != null)
        {
            // Interrompi la visualizzazione dell'oggetto
            raccoltaScript.StopViewing();
        }
        else
        {
            Debug.LogWarning("L'interactor non ha il componente Raccolta_Vedi_Oggetto!");
        }
    }
}
