using System;
using System.Collections;
using UnityEngine;

public class KnifePlacement : MonoBehaviour
{
    public Transform handTransform;       // Posizione della mano (dove il coltello è tenuto)
    public float transitionDuration = 1f; // Durata della transizione verso il punto di posa
    public bool isHoldingKnife = true;   // Stato: il coltello è nella mano?

    private Rigidbody knifeRigidbody;    // Riferimento al Rigidbody del coltello
    private Collider knifeCollider;      // Riferimento al Collider del coltello

    // Evento che segnala la posa del coltello
    public event Action OnKnifePlaced;

    private void Start()
    {
        // Recupera i componenti
        knifeRigidbody = GetComponent<Rigidbody>();
        knifeCollider = GetComponent<Collider>();

        if (knifeRigidbody != null)
        {
            knifeRigidbody.isKinematic = true;
            knifeRigidbody.useGravity = false;
        }

        if (knifeCollider == null)
        {
            Debug.LogError("Collider non trovato sul coltello.");
        }
    }

    private void Update()
    {
        // Se il coltello è in mano e appartiene al layer "PlayerKnife", controlla se il giocatore vuole posarlo
        if (isHoldingKnife && gameObject.layer == LayerMask.NameToLayer("PlayerKnife") && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(PlaceKnife());
        }
    }

    private IEnumerator PlaceKnife()
    {
        Debug.Log("Posa del coltello iniziata.");

        isHoldingKnife = false;

        // Stacca il coltello dalla mano
        transform.SetParent(null); // Rende il coltello un oggetto indipendente

        // Disabilita temporaneamente il collider per evitare problemi durante la transizione
        if (knifeCollider != null)
        {
            knifeCollider.enabled = false;
        }

        // Esegui un raycast dalla posizione della camera verso la direzione in cui sta guardando
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPosition = transform.position;
        Quaternion targetRotation = transform.rotation;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
            targetRotation = Quaternion.LookRotation(hit.normal);
        }

        // Calcola il tempo trascorso per l'animazione
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            // Interpola la posizione e la rotazione verso la posizione corrente
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Assicurati che il coltello sia esattamente nella posizione e rotazione finale
        transform.position = targetPosition;
        transform.rotation = targetRotation;

        Debug.Log("Posa del coltello completata.");

        // Riabilita il collider e la fisica
        if (knifeCollider != null)
        {
            knifeCollider.enabled = true;
        }

        if (knifeRigidbody != null)
        {
            knifeRigidbody.isKinematic = false;
            knifeRigidbody.useGravity = true;
        }

        if (!isHoldingKnife)
        {
            OnKnifePlaced?.Invoke();
        }
    }
}
