using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float boxLength = 5f; // La lunghezza della box nella direzione della camera
    public float boxWidth = 1f; // La larghezza della box (asse X)
    public float boxHeight = 1f; // L'altezza della box (asse Y)
    public LayerMask interactableLayer;
    public bool playAudio;
    public AttivaUi attivaUi;
    public IInteractable interactable;
    [SerializeField] private Collider[] colliders;

    public float interactionCooldown = 3f;
    private float currentCooldown;

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
                currentCooldown -= Time.deltaTime;
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

    private void CheckForInteractableObject()
    {
        Vector3 boxCenter = playerCamera.transform.position + playerCamera.transform.forward * (boxLength / 2);
        Vector3 halfExtents = new Vector3(boxWidth / 2, boxHeight / 2, boxLength / 2);
        Quaternion boxRotation = playerCamera.transform.rotation;
        int numColliders = Physics.OverlapBoxNonAlloc(boxCenter, halfExtents, colliders, boxRotation, interactableLayer);

        interactable = null;

        for (int i = 0; i < numColliders; i++)
        {
            Collider collider = colliders[i];
            IInteractable potentialInteractable = collider.GetComponent<IInteractable>();
            if (potentialInteractable != null)
            {
                interactable = potentialInteractable;
                break;
            }
        }
       
    }

 

    private void PerformInteraction()
    {
        if (interactable != null)
        {
            interactable.Interact(gameObject);
        }
    }

    private void StopInteraction()
    {
        if (interactable != null)
        {
            interactable.StopInteract(gameObject);
            interactable = null;
        }
    }

    void ChangeState(InteractionState newState)
    {
        if (newState == InteractionState.Interact)
        {
            currentCooldown = interactionCooldown;
        }
        currentState = newState;
    }

    private void OnDrawGizmosSelected()
    {
        if (playerCamera != null)
        {
            Vector3 boxCenter = playerCamera.transform.position + playerCamera.transform.forward * (boxLength / 2);
            Vector3 halfExtents = new Vector3(boxWidth / 2, boxHeight / 2, boxLength / 2);
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(boxCenter, playerCamera.transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2);
        }
    }
}
