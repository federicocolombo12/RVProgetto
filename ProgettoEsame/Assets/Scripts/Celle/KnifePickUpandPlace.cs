using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePickUpandPlace : MonoBehaviour
{
    private GameObject pickedObject = null;
    public Transform holdPosition;
    public LayerMask interactableLayer;
    private float animationSpeed = 5.0f;
    private float maxPickupDistance = 3.0f;
    private float maxDropDistance = 3.0f;

    // Cooldown variables
    public float interactionCooldown = 3f;
    private float currentCooldown;

    private enum InteractionState { Idle, Interact, StopInteract }
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
                // Only allow stopping interaction after cooldown
                if (currentCooldown <= 0 && Input.GetKeyDown(KeyCode.F))
                {
                    ChangeState(InteractionState.StopInteract);
                }
                break;

            case InteractionState.StopInteract:
                ChangeState(InteractionState.Idle);
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
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxPickupDistance, interactableLayer))
        {
            pickedObject = hit.transform.gameObject;

            MeshCollider meshCollider = pickedObject.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                meshCollider.enabled = false;
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
            StartCoroutine(DropObject(pickedObject, dropPosition, hit));
            pickedObject = null;
            ChangeState(InteractionState.Interact);
        }
    }

    private IEnumerator PickupObject(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }

        obj.transform.position = targetPosition;
        obj.transform.parent = holdPosition;

        CellaManager.instance.coltelloPreso = true;
        CellaManager.instance.attivaGuardRoutine = true;
    }

    private IEnumerator DropObject(GameObject obj, Vector3 targetPosition, RaycastHit hit)
    {
        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.enabled = true;
            meshCollider.convex = true;
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

        if (hit.transform.CompareTag("Drawer"))
        {
            obj.transform.parent = hit.transform;
        }

        CellaManager.instance.coltelloNascosto = true;
    }
    public void ActivateKnifeTag()
    {
        gameObject.tag = "OggettoInteragibile2";
    }
}
