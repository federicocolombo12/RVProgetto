using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raccolta_Vedi_Oggetto : MonoBehaviour
{
    public float interactionDistance = 2f;
    public float transitionDuration = 1f; // Durata della transizione
    public bool isFlat = false; // Variabile per indicare se l'oggetto è coricato
    private bool isViewing = false;
    private Transform player;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Renderer objectRenderer;
    private FirstPersonController playerController; // Riferimento al FirstPersonController
    public LayerMask interactableLayer; // Layer per gli oggetti interagibili
<<<<<<< Updated upstream

    // Nuove variabili per il voice over e la musica di sottofondo
    public AudioSource audioSource;  // AudioSource per voice over
    public AudioClip voiceOverClip;  // Clip audio del voice over
    private bool isVoiceOverPlaying = false;

    public AudioSource backgroundMusicSource;  // AudioSource per la musica di sottofondo
    public AudioClip backgroundMusicClip;      // Clip audio della musica di sottofondo
=======
    private Collider objectCollider;
    public bool playAudio;
    
>>>>>>> Stashed changes

    void Start()
    {
        // Assicurati che l'oggetto non sia statico
<<<<<<< Updated upstream
        gameObject.isStatic = false; // per ora fai così, ma poi basta levare static al prefab dell'oggetto e questa riga si può eliminare

=======
        gameObject.isStatic = false; // per ora fai cosi, ma poi basta levare static al prefab dell'oggetto e questa riga si può eliminare
        playAudio = false;
        
>>>>>>> Stashed changes
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            player = mainCamera.transform;
            playerController = player.GetComponentInParent<FirstPersonController>(); // Assumi che il FirstPersonController sia sul genitore della camera
        }
        else
        {
            Debug.LogError("Main Camera non trovata. Assicurati che la tua scena abbia una camera con il tag 'MainCamera'.");
        }

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer non trovato sull'oggetto. Assicurati che l'oggetto abbia un componente Renderer.");
        }
    }

    void Update()
    {
        if (isViewing)
        {
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(ExitView());
            }
        }
        else
        {
            CheckForPlayer();
        }
    }

    void CheckForPlayer()
    {
        if (player == null || objectRenderer == null)
        {
            return;
        }

        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // ricordati di mettere il layer Interactable agli oggetti su Unity
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);
            if (hit.transform == this.transform)
            {
                Debug.Log("Giocatore sta guardando l'oggetto.");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Tasto E premuto.");
                    StartCoroutine(EnterView());
                }
            }
        }
    }

    IEnumerator EnterView()
    {
        if (isViewing) yield break;

        isViewing = true;
        Debug.Log("Entrato in modalità visualizzazione.");

        // Disabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Salva la posizione e la rotazione originali dell'oggetto
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;

        // Calcola la posizione target
        Vector3 targetPosition = player.position + player.forward * 0.6f; // Posiziona l'oggetto molto vicino alla camera

        // Calcola la rotazione target per far guardare l'oggetto verso la camera
        Quaternion targetRotation = Quaternion.LookRotation(player.position - this.transform.position);

        // Aggiungi un offset di rotazione in base all'orientamento dell'oggetto
        if (isFlat)
        {
            targetRotation *= Quaternion.Euler(90, 0, 0);
        }
        else
        {
            targetRotation *= Quaternion.Euler(0, 180, 0);
        }

        Debug.Log("Posizione target: " + targetPosition);
        Debug.Log("Rotazione target: " + targetRotation);

        // Assicurati che l'oggetto sia visibile
        objectRenderer.enabled = true;

        // Transizione fluida verso la posizione e la rotazione target
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            this.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / transitionDuration);
            this.transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.transform.position = targetPosition;
        this.transform.rotation = targetRotation;

        // Avvia il voice over
        if (voiceOverClip != null)
        {
            isVoiceOverPlaying = true;
            audioSource.clip = voiceOverClip;
            audioSource.Play();
        }

        // Avvia la musica di sottofondo (se non è già in riproduzione)
        if (backgroundMusicClip != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.Play();
        }

        Debug.Log("Oggetto posizionato davanti al giocatore.");
    }

    IEnumerator ExitView()
    {
        if (!isViewing) yield break;

        isViewing = false;
        Debug.Log("Uscito dalla modalità visualizzazione.");

        // Ferma il voice over
        if (isVoiceOverPlaying && audioSource.isPlaying)
        {
            audioSource.Stop();
            isVoiceOverPlaying = false;
        }

        // Ferma la musica di sottofondo
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }

        // Riabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Transizione fluida verso la posizione e la rotazione originali
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, originalPosition, elapsedTime / transitionDuration);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, originalRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.transform.position = originalPosition;
        this.transform.rotation = originalRotation;

        Debug.Log("Oggetto riposizionato nella posizione originale.");
    }

    void RotateObject()
    {
        float rotationSpeed = 100f;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        if (isFlat)
        {
            // Ruota l'oggetto coricato sull'asse Z
            this.transform.Rotate(Vector3.forward, mouseX, Space.Self);
        }
        else
        {
            // Ruota l'oggetto in piedi sull'asse Y
            this.transform.Rotate(Vector3.up, mouseX, Space.Self);
        }
    }
}
