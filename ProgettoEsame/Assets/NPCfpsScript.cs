using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCfpsScript : MonoBehaviour
{
    public Camera mainCamera;     // Riferimento alla telecamera del giocatore
    [SerializeField] private float interactionDistance = 2f; // Distanza massima di interazione
    [SerializeField] private LayerMask npcLayer;    // Layer degli NPC

    private void Update()
    {
        // Controlla se il giocatore preme il tasto E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteractWithNPC();
        }
    }

    private void TryInteractWithNPC()
    {
        // Crea un raggio dalla posizione del giocatore in avanti
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactionDistance, Color.red, 1.0f);

        // Controlla se il raggio colpisce un oggetto nel layer degli NPC
        if (Physics.Raycast(ray, out hit, interactionDistance, npcLayer))
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);

            // Ottieni il componente Paziente0Script dall'oggetto colpito
            Paziente0Script paziente0Script = hit.transform.GetComponent<Paziente0Script>();
            if (paziente0Script != null)
            {
                // Attiva l'interazione nel Paziente0Script
                paziente0Script.Interact();
            }
        }
        else
        {
            Debug.Log("Raycast non ha colpito nulla.");
        }
    }
}
