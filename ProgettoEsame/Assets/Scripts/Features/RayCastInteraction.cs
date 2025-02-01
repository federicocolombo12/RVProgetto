using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    public float interactionDistance = 2f;
    public LayerMask interactableLayer;
    public bool playAudio;
    public AttivaUi attivaUi;
    public IInteractable interactable;
    [SerializeField] private Collider[] colliders;
    

    // Cooldown variables
    public float interactionCooldown = 3f; // Time before allowing StopInteract
    private float currentCooldown;

    private enum InteractionState { Idle, Interact, StopInteract }
    private InteractionState currentState = InteractionState.Idle;

    void Start()
    {
        
    }

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
                // Update cooldown timer
                currentCooldown -= Time.deltaTime;
                // Only allow stopping interaction after cooldown
                if (currentCooldown <= 0 && Input.GetKeyDown(KeyCode.E))
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
        // Reset cooldown when entering Interact state
        if (newState == InteractionState.Interact)
        {
            currentCooldown = interactionCooldown;
        }
        currentState = newState;
    }

    void CheckForInteractableObject()
    {
        // Offset della sfera leggermente davanti al giocatore
        float forwardOffset = 0.5f;
        Vector3 spherePosition = transform.position + transform.forward * forwardOffset;

        // Disegna la sfera nel debug per visualizzarla nella scena
        Debug.DrawLine(transform.position, spherePosition, Color.red);
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);

        // Trova i collider entro la distanza specificata
        colliders = Physics.OverlapSphere(spherePosition, interactionDistance, interactableLayer);
        
        // Inizialmente, non c'è un oggetto interagibile
        interactable = null;

        // Itera tra i collider trovati
        foreach (var collider in colliders)
        {
            // Controlla se il collider ha un componente che implementa IInteractable
            IInteractable potentialInteractable = collider.GetComponent<IInteractable>();
            if (collider.gameObject.GetComponentInChildren<MeshRenderer>(true) != null)
                { 
                collider.gameObject.GetComponentInChildren<MeshRenderer>(true).enabled = true; 
            }
            if (potentialInteractable != null)
            {
                // Verifica se il raycast punta effettivamente a questo oggetto
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
                {
                    // Se il raycast colpisce lo stesso oggetto, lo assegna come interagibile
                    if (hit.collider == collider)
                    {
                        interactable = potentialInteractable;
                        break; // Termina il ciclo, poiché hai trovato l'oggetto
                    }
                }
            }
        }

        // Aggiorna la UI in base al risultato
        if (interactable != null)
        {
            //UiManager.instance.vedi = true;
            //attivaUi.Vedi();
        }
        else
        {
            //UiManager.instance.vedi = false;
            //attivaUi.Vedi();
        }
    

    // Debug: Disegna la sfera nel Scene View

    // Controlla se il raggio colpisce un oggetto interagibile
    /*if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
    {
        // L'oggetto interagibile è stato colpito
        UiManager.instance.vedi = true;
        attivaUi.Vedi();

        // Salva l'oggetto interagibile
        interactable = hit.collider.GetComponent<IInteractable>();
    }
    else
    {
        // Nessun oggetto interagibile colpito
        UiManager.instance.vedi = false;
        attivaUi.Vedi();
        interactable = null;
    }*/
}

    void PerformInteraction()
    {
        if (interactable != null)
        {
            
            interactable.Interact(gameObject); 
            // Attiva il Canvas quando l'oggetto viene visualizzato

        }
    }

    void StopInteraction()
    {
        if (interactable != null)
        {
            interactable.StopInteract(gameObject); // Richiama il metodo di interruzione
            interactable = null;

            // Disattiva il Canvas quando la visualizzazione termina
            
        }
    }
}