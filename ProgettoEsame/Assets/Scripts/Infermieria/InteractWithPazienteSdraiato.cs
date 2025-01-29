using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithPazienteSdraiato : MonoBehaviour
{
    [SerializeField] private PazienteSdraiato pazienteSdraiato;  // Riferimento allo script PazienteSdraiato
    private Transform player;
    [SerializeField] private float interactionDistance = 2f;  // Distanza di interazione
    [SerializeField] private LayerMask interactableLayer;  // Layer dell'oggetto con cui interagire (assicurati che il paziente abbia il layer giusto)

    void Start()
    {
        player = Camera.main.transform;  // Ottieni la posizione della telecamera del giocatore
    }

    void Update()
    {
        // Chiamata alla funzione di interazione
        TalkWithPatient();
    }

    void TalkWithPatient()
    {
        Ray ray = new Ray(player.position, player.forward);  // Creazione del raggio dal giocatore in avanti
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.green);

        // Se il raggio colpisce un oggetto dentro la distanza specificata
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // Controlla se il raycast ha colpito il paziente
            if (hit.transform == pazienteSdraiato.transform)
            {
                // Quando il giocatore è abbastanza vicino, e preme il tasto "E"
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Avvia l'interazione con il paziente
                    pazienteSdraiato.StartInteraction();
                }
            }
        }
    }
}
