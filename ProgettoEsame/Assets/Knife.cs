using System.Collections;
using UnityEngine;

public class KnifePlacement : MonoBehaviour
{
    public Transform handTransform;       // Posizione della mano (dove il coltello è tenuto)
    public Transform dropPoint;          // Posizione in cui il coltello verrà posato
    public float transitionDuration = 1f; // Durata della transizione verso il punto di posa
    public bool isHoldingKnife = true;   // Stato: il coltello è nella mano?

    private Rigidbody knifeRigidbody;    // Riferimento al Rigidbody del coltello
    private Collider knifeCollider;      // Riferimento al Collider del coltello
    private Vector3 originalDropPosition; // Posizione finale dove posare il coltello
    private Quaternion originalDropRotation; // Rotazione orizzontale finale del coltello

    private void Start()
    {
        // Recupera i componenti
        knifeRigidbody = GetComponent<Rigidbody>();
        knifeCollider = GetComponent<Collider>();

        if (knifeRigidbody != null)
        {
            // Disabilita la fisica mentre il coltello è in mano
            knifeRigidbody.isKinematic = true;
            knifeRigidbody.useGravity = false;
        }

        if (knifeCollider == null)
        {
            Debug.LogError("Collider non trovato sul coltello.");
        }

        // Salva la posizione e la rotazione finale per il punto di posa
        originalDropPosition = dropPoint.position;
        originalDropRotation = Quaternion.Euler(90, 0, 90); // Rotazione orizzontale del coltello
    }

    private void Update()
    {
        // Se il coltello è in mano, controlla se il giocatore vuole posarlo
        if (isHoldingKnife && Input.GetKeyDown(KeyCode.F))
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

        // Calcola il tempo trascorso per l'animazione
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            // Interpola la posizione e la rotazione verso il punto di posa
            transform.position = Vector3.Lerp(startPosition, originalDropPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(startRotation, originalDropRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Assicurati che il coltello sia esattamente nella posizione e rotazione finale
        transform.position = originalDropPosition;
        transform.rotation = originalDropRotation;

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
    }
}
