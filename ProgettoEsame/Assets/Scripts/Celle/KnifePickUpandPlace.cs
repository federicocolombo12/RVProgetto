using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePickUpandPlace : MonoBehaviour
{
    private GameObject pickedObject = null;
    public Transform holdPosition;
    public LayerMask interactableLayer; // LayerMask per gli oggetti interagibili
    private float animationSpeed = 5.0f; // Velocità di animazione per raccogliere e posare l'oggetto
    private float maxPickupDistance = 3.0f; // Distanza massima per raccogliere l'oggetto
    private float maxDropDistance = 3.0f; // Distanza massima per posare l'oggetto


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (pickedObject == null)
            {
                // Prova a raccogliere un oggetto
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxPickupDistance, interactableLayer))
                {
                    Debug.Log("Raycast hit: " + hit.transform.name);
                    pickedObject = hit.transform.gameObject;

                    // Disabilita il MeshCollider per evitare problemi di fisica
                    MeshCollider meshCollider = pickedObject.GetComponent<MeshCollider>();
                    if (meshCollider != null)
                    {
                        meshCollider.enabled = false;
                    }

                    // Disabilita il Rigidbody per evitare che cada mentre è tenuto
                    Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }

                    // Inizia la coroutine per animare l'oggetto verso la posizione di raccolta
                    StartCoroutine(PickupObject(pickedObject, holdPosition.position));

                    Debug.Log("Picked up: " + pickedObject.name);
                }
                else
                {
                    Debug.Log("Raycast did not hit any object");
                }
            }
            else
            {
                // Trova la posizione in cui stai guardando
                RaycastHit hit;
                Vector3 dropPosition = pickedObject.transform.position;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDropDistance))
                {
                    dropPosition = hit.point;
                }

                // Verifica se la distanza di rilascio è entro il limite
                if (Vector3.Distance(transform.position, dropPosition) <= maxDropDistance)
                {
                    // Rilascia l'oggetto
                    pickedObject.transform.parent = null;

                    // Inizia la coroutine per animare l'oggetto verso la posizione di rilascio
                    StartCoroutine(DropObject(pickedObject, dropPosition, hit));

                    Debug.Log("Dropped: " + pickedObject.name);
                    pickedObject = null;
                }
                else
                {
                    Debug.Log("Drop position is too far away");
                }
            }
        }
    }

    private IEnumerator PickupObject(GameObject obj, Vector3 targetPosition)
    {
        // Anima l'oggetto verso la posizione di raccolta
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }

        // Imposta la posizione finale e il parent
        obj.transform.position = targetPosition;
        obj.transform.parent = holdPosition;

        // Verifica se l'oggetto raccolto è il coltello

        Debug.Log("Coltello raccolto");
        CellaManager.instance.coltelloPreso = true;
        CellaManager.instance.attivaGuardRoutine = true;
    }

    private IEnumerator DropObject(GameObject obj, Vector3 targetPosition, RaycastHit hit)
    {
        // Riabilita il MeshCollider
        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.enabled = true;
            meshCollider.convex = true; // Rendi il MeshCollider convesso
        }

        // Aggiungi un Rigidbody per far cadere l'oggetto
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true; // Rendi il Rigidbody cinematico per l'animazione

        // Anima l'oggetto verso la posizione di rilascio
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }

        // Disabilita il Rigidbody per far cadere l'oggetto
        rb.isKinematic = false;

        // Verifica se l'oggetto è stato posato nel cassetto
        if (hit.transform.CompareTag("Drawer"))
        {
            obj.transform.parent = hit.transform;
        }

        CellaManager.instance.coltelloNascosto = true;
    }
}
