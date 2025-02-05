using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorciaOnOffUI : MonoBehaviour
{
    [SerializeField] private Image imgOn;  // Immagine quando la torcia è accesa
    [SerializeField] private Image imgOff; // Immagine quando la torcia è spenta

    [SerializeField] private GameObject torciaGameObject; // GameObject che contiene il componente Torcia
    private Torcia gestioneTorcia;

    void Start()
    {
        // Trova il componente Torcia nel GameObject specificato
        if (torciaGameObject != null)
        {
            gestioneTorcia = torciaGameObject.GetComponent<Torcia>();
            if (gestioneTorcia == null)
            {
                Debug.LogError("Nessun componente Torcia trovato nel GameObject specificato.");
            }
        }
        else
        {
            Debug.LogError("Nessun GameObject specificato per la torcia.");
        }
    }

    void Update()
    {
        if (gestioneTorcia != null) // Controlla se la torcia esiste
        {
            if (gestioneTorcia.on)
            {
                imgOn.gameObject.SetActive(true);
                imgOff.gameObject.SetActive(false);
                Debug.Log("UI Torcia accesa");
            }
            else if (gestioneTorcia.off)
            {
                imgOn.gameObject.SetActive(false);
                imgOff.gameObject.SetActive(true);
                Debug.Log("UI Torcia spenta");
            }
            else
            {
                Debug.Log("Script non preso");
            }
        }
        else
        {
            Debug.LogWarning("La torcia non è stata assegnata!");
        }
    }
}

