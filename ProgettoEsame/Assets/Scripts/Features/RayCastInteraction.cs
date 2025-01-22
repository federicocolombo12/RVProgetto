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

    // Enum per gestire gli stati
    private enum InteractionState { Idle, Interact, StopInteract }
    private InteractionState currentState = InteractionState.Idle;

    void Start()
    {
        // Trova il gestore dell'interfaccia utente
        attivaUi = FindObjectOfType<AttivaUi>();
        if (attivaUi == null)
        {
            Debug.LogError("AttivaUi non trovata. Assicurati che sia presente nella scena.");
        }
    }

    void Update()
    {
        // Esegui le azioni in base allo stato corrente
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
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);

        // Controlla se il raggio colpisce un oggetto interagibile
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // L'oggetto interagibile è stato colpito
            UiManager.instance.vedi = true;
            attivaUi.Vedi();

            // Salva l'oggetto interagibile
            interactable = hit.transform.GetComponent<IInteractable>();
        }
        else
        {
            // Nessun oggetto interagibile colpito
            UiManager.instance.vedi = false;
            attivaUi.Vedi();
            interactable = null;
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
        }
    }

}