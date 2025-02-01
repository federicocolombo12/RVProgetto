using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vedi_Oggetto : MonoBehaviour, IInteractable
{
    [SerializeField] Raccolta_Vedi_Oggetto raccoltaScript;
    [SerializeField] Canvas canvasVisualizzazione;
    public bool stopView = false;
    void Start() 
    {
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
            if (canvasVisualizzazione != null)
            {
                canvasVisualizzazione.gameObject.SetActive(true);
            }

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
        if (canvasVisualizzazione != null)
        {
            canvasVisualizzazione.gameObject.SetActive(false);
        }
    }

}
