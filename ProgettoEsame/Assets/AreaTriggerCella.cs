using UnityEngine;

public class TriggerAreaManager : MonoBehaviour
{
    private KnifePickUpandPlace knifeScript;

    private void Start()
    {
        // Trova lo script KnifePickUpandPlace che è attaccato al giocatore o all'oggetto desiderato
        knifeScript = FindObjectOfType<KnifePickUpandPlace>();
        if (knifeScript == null)
        {
            Debug.LogError("TriggerAreaManager: Non è stato possibile trovare lo script KnifePickUpandPlace.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Controlla se il collider che entra è etichettato come "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Il player è entrato nell'area del trigger.");
            // Imposta lo stato del trigger nel knifeScript
            if (knifeScript != null)
                knifeScript.SetPlayerInTriggerArea(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Il player ha lasciato l'area del trigger.");
            // Reimposta lo stato del trigger nel knifeScript
            if (knifeScript != null)
                knifeScript.SetPlayerInTriggerArea(false);
        }
    }
}
