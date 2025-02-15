using System.Collections;
using UnityEngine;

public class AttivaPorta : MonoBehaviour
{
    [SerializeField] private GameObject pickedObject = null;
    public Transform holdPosition;
    public LayerMask interactableLayer;
    private float animationSpeed = 5.0f;
    private float maxPickupDistance = 3.0f;
    private float maxDropDistance = 3.0f;
    [SerializeField] private FirstPersonController FirstPersonController;
    [SerializeField] private MedicineAudio medicineAudio;
    private bool notLoaded = true;
    // Cooldown variables
    public float interactionCooldown = 3f;
    private float currentCooldown;

    private enum InteractionState { Idle, Interact }
    private InteractionState currentState = InteractionState.Idle;

    // Box dimensions
    public float boxLength = 2f; // La lunghezza della box nella direzione della camera
    public float boxWidth = 0.1f; // La larghezza della box (asse X)
    public float boxHeight = 0.1f; // L'altezza della box (asse Y)

    void Update()
    {
        switch (currentState)
        {
            case InteractionState.Idle:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (pickedObject == null)
                    {
                        TryPickUpObject();
                    }
                    else
                    {
                        TryDropObject();
                    }
                }
                break;

            case InteractionState.Interact:
                // Update cooldown timer
                currentCooldown -= Time.deltaTime;
                if (currentCooldown <= 0)
                {
                    ChangeState(InteractionState.Idle);
                }
                break;
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

    void TryPickUpObject()
    {
        Vector3 boxCenter = Camera.main.transform.position + Camera.main.transform.forward * (maxPickupDistance / 2);
        Vector3 boxHalfExtents = new Vector3(boxWidth / 2, boxHeight / 2, boxLength / 2);
        Quaternion boxOrientation = Camera.main.transform.rotation;
        Collider[] hitColliders = new Collider[10]; // Array di colliders per memorizzare i risultati

        int numHits = Physics.OverlapBoxNonAlloc(boxCenter, boxHalfExtents, hitColliders, boxOrientation, interactableLayer);

        for (int i = 0; i < numHits; i++)
        {
            Collider hitCollider = hitColliders[i];
            if (hitCollider != null)
            {
                pickedObject = hitCollider.gameObject;

                if (pickedObject.TryGetComponent(out Collider collider))
                {
                    collider.enabled = false;
                }

                if (pickedObject.TryGetComponent(out Rigidbody rb))
                {
                    rb.isKinematic = true;
                }

                StartCoroutine(PickupObject(pickedObject, holdPosition.position));
                ChangeState(InteractionState.Interact);
                break; // Esci dal ciclo una volta trovato l'oggetto
            }
        }
    }

    void TryDropObject()
    {
        if (pickedObject == null) return;

        Vector3 dropPosition = pickedObject.transform.position;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxDropDistance))
        {
            dropPosition = hit.point;
        }

        if (Vector3.Distance(transform.position, dropPosition) <= maxDropDistance)
        {
            pickedObject.transform.parent = null;
            StartCoroutine(DropObject(pickedObject, dropPosition));
            pickedObject = null;
            ChangeState(InteractionState.Interact);
        }
    }

    private IEnumerator PickupObject(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            FirstPersonController.cameraCanMove = false;
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, holdPosition.rotation, animationSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.tag = "Untagged";
        obj.transform.position = targetPosition;

        obj.transform.parent = holdPosition;

        InfermieriaManager.instance.pastigliaTrovata = true;

        // Chiamata al metodo per riprodurre il suono
        if (medicineAudio != null)
        {
            medicineAudio.PlayMedicineSound(); // Gestito tramite lo script MedicineAudio
        }

        FirstPersonController.cameraCanMove = true;
        this.enabled = false; // Disattiva lo script dopo aver raccolto l'oggetto
    }

    private IEnumerator DropObject(GameObject obj, Vector3 targetPosition)
    {
        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;

        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }

        rb.isKinematic = false;
    }
    
}
