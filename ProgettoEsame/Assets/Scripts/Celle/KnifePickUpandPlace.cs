using System.Collections;
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

    // Variabili per i due AudioSource
    public AudioSource audioSource1; // Primo AudioSource (Empty)
    public AudioSource audioSource2; // Secondo AudioSource (Paziente Zero)

    // AudioClip per i due AudioSource
    public AudioClip knifePickupClip;    // Clip audio per il pickup del coltello (Empty)
    public AudioClip secondAudioClip;    // Clip audio per il paziente zero

    // Variabile per verificare se l'audio del coltello è in riproduzione
    public bool isKnifePickupAudioPlaying = false;

    private enum InteractionState { Idle, Interact, StopInteract }
    private InteractionState currentState = InteractionState.Idle;

    // Ritardi
    public float secondAudioDelay = 2f;  // Ritardo per il secondo audio (Paziente Zero)
    public float guardAudioDelay = 5f;  // Ritardo aggiuntivo per il suono della guardia

    void Start()
    {
        // Assicurati che gli AudioSource siano assegnati se non lo sono già
        if (audioSource1 == null)
        {
            audioSource1 = GetComponents<AudioSource>()[0]; // Prendi il primo AudioSource
        }
        if (audioSource2 == null)
        {
            audioSource2 = GetComponents<AudioSource>()[1]; // Prendi il secondo AudioSource
        }
    }

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

            // Attiva il primo audio immediatamente (senza ritardo)
            PlayKnifePickupAudio();

            // Attiva il secondo audio con il ritardo di 2 secondi e ritardo aggiuntivo per la guardia
            StartCoroutine(PlaySecondAudioWithDelay(secondAudioDelay + guardAudioDelay));

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

    private IEnumerator PlaySecondAudioWithDelay(float delay)
    {
        // Aggiungi il ritardo totale (2 secondi + ritardo per la guardia)
        yield return new WaitForSeconds(delay);

        // Riproduci il secondo audio (Paziente Zero)
        PlaySecondAudio();
    }

    private void PlayKnifePickupAudio()
    {
        if (audioSource1 != null && knifePickupClip != null && !isKnifePickupAudioPlaying)
        {
            audioSource1.clip = knifePickupClip;
            audioSource1.Play();
            isKnifePickupAudioPlaying = true;  // Imposta lo stato come vero quando l'audio viene riprodotto
        }
    }

    private void PlaySecondAudio()
    {
        if (audioSource2 != null && secondAudioClip != null)
        {
            audioSource2.clip = secondAudioClip;
            audioSource2.Play();
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

        // Disattiva i suoni dopo un certo tempo (5 secondi)
        yield return new WaitForSeconds(5f);
        audioSource1.Stop();
        audioSource2.Stop();
        isKnifePickupAudioPlaying = false;  // Reimposta lo stato quando l'audio finisce
    }

    private IEnumerator DropObject(GameObject obj, Vector3 targetPosition, RaycastHit hit)
    {
        // Verifica che l'oggetto non sia null
        if (obj == null)
        {
            Debug.LogError("L'oggetto da rilasciare è null!");
            yield break;  // Interrompi l'esecuzione se l'oggetto è null
        }

        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.enabled = true;
            meshCollider.convex = true;
        }
        else
        {
            Debug.LogWarning("MeshCollider non trovato su " + obj.name);
        }

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody non trovato su " + obj.name + ", aggiungo un nuovo Rigidbody.");
            rb = obj.AddComponent<Rigidbody>();  // Aggiungi un nuovo Rigidbody se non esiste
        }
        rb.isKinematic = true;

        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }

        rb.isKinematic = false;

        // Verifica che l'oggetto sia stato posizionato correttamente nel target
        if (hit.transform != null && hit.transform.CompareTag("Drawer"))
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
