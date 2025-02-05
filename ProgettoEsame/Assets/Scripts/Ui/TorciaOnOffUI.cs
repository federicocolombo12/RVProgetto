using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TorciaOnOffUI : MonoBehaviour
{
    [SerializeField] private Image imgOn;  // Immagine quando la torcia è accesa
    [SerializeField] private Image imgOff; // Immagine quando la torcia è spenta
    
    [SerializeField] private Torcia gestioneTorcia;

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

            //imgOn.gameObject.SetActive(isTorchOn);
            //imgOff.gameObject.SetActive(!isTorchOn);
        }
        else
        {
            Debug.LogWarning("La torcia non è stata assegnata!");
        }
    }
}
