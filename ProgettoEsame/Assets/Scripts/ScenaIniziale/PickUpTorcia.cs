using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpTorcia : MonoBehaviour
{
    
    private GameObject pickedObject = null;
    public Transform holdPosition;
    public LayerMask interactableLayer; // LayerMask per gli oggetti interagibili
    private float animationSpeed = 10.0f; // Velocità di animazione per raccogliere e posare l'oggetto
    private float maxPickupDistance = 3.0f; // Distanza massima per raccogliere l'oggetto
    private float maxDropDistance = 3.0f; // Distanza massima per posare l'oggetto
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isViewing = false;
    private bool isHolding = false;
    private FirstPersonController playerController; // Riferimento al FirstPersonController
    public bool isFlatObject = false; // Flag per determinare se l'oggetto è piatto
    public bool rotazione = false;
    [SerializeField] Transform targetPosition; // Posizione desiderata
    [SerializeField] float duration = 2.0f; // Durata dell'animazione

    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            playerController = mainCamera.GetComponentInParent<FirstPersonController>(); // Assumi che il FirstPersonController sia sul genitore della camera
        }
        else
        {
            Debug.LogError("Main Camera non trovata. Assicurati che la tua scena abbia una camera con il tag 'MainCamera'.");
        }
    }

    public void StartReturnAndDestroy(GameObject interactor)
    {
        // Esempio di destinazione (modifica secondo le tue necessità)
         // Rotazione desiderata

        // Avvia la coroutine per riportare l'oggetto indietro e distruggerlo
        StartCoroutine(AnimateAndDestroy(gameObject, targetPosition.position, targetPosition.rotation, duration, interactor));
        
    }

    public IEnumerator AnimateAndDestroy(GameObject obj, Vector3 targetPosition, Quaternion targetRotation, float duration, GameObject interactor)
    {
        if (obj == null)
        {
            Debug.LogError("L'oggetto passato a AnimateAndDestroy è null.");
            yield break;
        }

        Vector3 initialPosition = obj.transform.position;
        Quaternion initialRotation = obj.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (obj == null)
            {
                Debug.LogWarning("L'oggetto è stato distrutto durante l'animazione.");
                yield break;
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpolazione della posizione e della rotazione
            obj.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            obj.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

            yield return null;
        }

        // Distruzione dell'oggetto
        if (obj != null)
        {
            Destroy(obj);
            interactor.gameObject.GetComponent<FirstPersonController>().enabled = true;
            Debug.Log("Oggetto distrutto: " + obj.name);
        }
    }
}