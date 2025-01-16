using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Collider objectCollider;

    // Variabili per il voice over e la musica di sottofondo
    public AudioSource audioSource;  // AudioSource per voice over
    public AudioClip voiceOverClip;  // Clip audio del voice over
    private bool isVoiceOverPlaying = false;

    public AudioSource backgroundMusicSource;  // AudioSource per la musica di sottofondo
    public AudioClip backgroundMusicClip;      // Clip audio della musica di sottofondo

    // Nuova variabile per la musica d'ambiente
    public AudioSource ambientMusicSource;  // AudioSource per la musica d'ambiente
    public float ambientMusicFadeDuration = 1f; // Durata della transizione del volume
    private float originalAmbientVolume; // Volume originale della musica d'ambiente

    void Start()
    {
        gameObject.isStatic = false;

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            player = mainCamera.transform;
            playerController = player.GetComponentInParent<FirstPersonController>();
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

        if (ambientMusicSource != null)
        {
            originalAmbientVolume = ambientMusicSource.volume; // Salva il volume originale
        }

        objectCollider = GetComponent<Collider>();
        if (objectCollider == null)
        {
            Debug.LogError("Collider non trovato sull'oggetto. Assicurati che l'oggetto abbia un componente Collider.");
        }
    }

    void Update()
    {
        if (isViewing)
        {
            RotateObject();
            if (Input.GetMouseButtonDown(1))
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

        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
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

        // Disabilita il collider dell'oggetto
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        // Salva la posizione e la rotazione originali dell'oggetto
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;

        Vector3 targetPosition = player.position + player.forward * 0.6f;
        Quaternion targetRotation = Quaternion.LookRotation(player.position - this.transform.position);

        if (isFlat)
        {
            targetRotation *= Quaternion.Euler(90, 0, 0);
        }
        else
        {
            targetRotation *= Quaternion.Euler(0, 180, 0);
        }

        objectRenderer.enabled = true;

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

        // Abbassa gradualmente il volume della musica d'ambiente
        if (ambientMusicSource != null)
        {
            StartCoroutine(FadeOutAmbientMusic());
        }

        // Avvia il voice over
        if (voiceOverClip != null)
        {
            isVoiceOverPlaying = true;
            audioSource.clip = voiceOverClip;
            audioSource.Play();
        }

        // Avvia la musica di sottofondo
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

        // Ripristina gradualmente il volume della musica d'ambiente
        if (ambientMusicSource != null)
        {
            StartCoroutine(FadeInAmbientMusic());
        }

        // Riabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = true;
        }

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

        // Riabilita il collider dell'oggetto
        if (objectCollider != null)
        {
            objectCollider.enabled = true;
        }

        Debug.Log("Oggetto riposizionato nella posizione originale.");
    }

    IEnumerator FadeOutAmbientMusic()
    {
        float startVolume = ambientMusicSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < ambientMusicFadeDuration)
        {
            ambientMusicSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / ambientMusicFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ambientMusicSource.volume = 0f;
    }

    IEnumerator FadeInAmbientMusic()
    {
        float elapsedTime = 0f;

        while (elapsedTime < ambientMusicFadeDuration)
        {
            ambientMusicSource.volume = Mathf.Lerp(0f, originalAmbientVolume, elapsedTime / ambientMusicFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ambientMusicSource.volume = originalAmbientVolume;
    }

    void RotateObject()
    {
        float rotationSpeed = 100f;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        if (isFlat)
        {
            this.transform.Rotate(Vector3.forward, mouseX, Space.Self);
        }
        else
        {
            this.transform.Rotate(Vector3.up, mouseX, Space.Self);
        }
    }
}
