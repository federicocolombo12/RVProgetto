using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Distanza massima per l'interazione
    public LayerMask interactableLayer; // Layer per gli oggetti interagibili
    public bool playAudio; // Per gestire l'audio
    public AttivaUi attivaUi; // Gestore dell'interfaccia utente
    [SerializeField] private IInteractable interactable;
    [SerializeField] private Collider interactableCollider; // Collider visibile nell'Inspector

    // Enum per gestire gli stati
    private enum InteractionState { Idle, Interact, StopInteract }
    private InteractionState currentState = InteractionState.Idle;

    void Update()
    {
        switch (currentState)
        {
            case InteractionState.Idle:
                CheckForInteractableObject();
                if (Input.GetKeyDown(KeyCode.E) && interactable != null)
                {
                    ChangeState(InteractionState.Interact);
                }
                break;

            case InteractionState.Interact:
                PerformInteraction();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ChangeState(InteractionState.StopInteract);
                }
                break;

            case InteractionState.StopInteract:
                StopInteraction();
                ChangeState(InteractionState.Idle);
                break;
        }
    }

    void ChangeState(InteractionState newState)
    {
        currentState = newState;
    }

    void CheckForInteractableObject()
    {
        // Crea un raggio dalla posizione del giocatore nella direzione in cui sta guardando
        Transform player = Camera.main.transform;
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.green);

        // Controlla se il raggio colpisce un oggetto interagibile
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // Salva il riferimento al componente interagibile
            interactable = hit.transform.GetComponent<IInteractable>();

            // Salva il collider dell'oggetto colpito
            interactableCollider = hit.collider;
        }
        else
        {
            // Resetta i riferimenti se non c'è nulla
            interactable = null;
            interactableCollider = null;
        }
    }

    void PerformInteraction()
    {
        if (interactable != null)
        {
            playAudio = true;
            interactable.Interact(gameObject); // Richiama il metodo di visualizzazione
        }
    }

    void StopInteraction()
    {
        if (interactable != null)
        {
            interactable.StopInteract(gameObject); // Richiama il metodo di interruzione
            interactable = null;
            interactableCollider = null;
        }
    }
}
