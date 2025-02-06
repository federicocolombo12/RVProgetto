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

    // Cooldown variables
    public float interactionCooldown = 3f;
    private float currentCooldown;

    private enum InteractionState { Idle, Interact }
    private InteractionState currentState = InteractionState.Idle;

    void Update()
    {
        switch (currentState)
        {
            case InteractionState.Idle:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (pickedObject == null)
                    {
                        TryPickupObject();
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

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxPickupDistance, interactableLayer))
        {
            pickedObject = hit.transform.gameObject;
            Collider collider = pickedObject.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            StartCoroutine(PickupObject(pickedObject, holdPosition.position));
            ChangeState(InteractionState.Interact);
        }
    }

    void TryDropObject()
    {
        RaycastHit hit;
        Vector3 dropPosition = pickedObject.transform.position;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDropDistance))
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tavolo"))
        {
            Debug.Log("Oggetto Posato");
            InfermieriaManager.instance.medicinaPresa = true;
        }
        else
        {
            Debug.Log("Oggetto non posato");
        }
    }
}
