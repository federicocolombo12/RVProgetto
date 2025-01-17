using UnityEngine;
using System.Collections;

public class VoiceOverObject : MonoBehaviour
{
    public AudioSource voiceOverAudioSource;       // L'AudioSource per riprodurre il voice over
    public AudioSource ambientMusicSource;         // L'AudioSource per la musica d'ambiente
    public AudioSource objectMusicSource;          // L'AudioSource per la musica di sottofondo dell'oggetto

    public AudioClip voiceOverClip;                // Clip audio del voice over quando si interagisce con l'oggetto
    public AudioClip ambientMusicClip;             // Clip audio della musica d'ambiente
    public AudioClip objectMusicClip;              // Clip audio della musica dell'oggetto

    public string objectDescription;               // Descrizione opzionale dell'oggetto
    public float interactionDistance = 3f;         // Distanza massima per interagire con l'oggetto
    public float fadeDuration = 1f;                // Durata della dissolvenza in secondi

    private Transform player;                      // Riferimento al giocatore
    private bool isInteracting = false;            // Stato di interazione con l'oggetto
    private bool canInteract = false;              // Verifica se il giocatore può interagire con l'oggetto

    void Start()
    {
        player = Camera.main.transform;            // Supponiamo che la fotocamera sia controllata dal giocatore

        // Configura l'AudioSource per il voice over
        if (voiceOverAudioSource == null)
        {
            voiceOverAudioSource = gameObject.AddComponent<AudioSource>();
        }
        voiceOverAudioSource.playOnAwake = false;

        // Configura l'AudioSource per la musica d'ambiente
        if (ambientMusicSource == null)
        {
            ambientMusicSource = gameObject.AddComponent<AudioSource>();
        }
        ambientMusicSource.clip = ambientMusicClip;
        ambientMusicSource.loop = true;
        ambientMusicSource.playOnAwake = true;
        if (ambientMusicClip != null)
        {
            ambientMusicSource.Play(); // Avvia la musica d'ambiente se presente
        }

        // Configura l'AudioSource per la musica dell'oggetto
        if (objectMusicSource == null)
        {
            objectMusicSource = gameObject.AddComponent<AudioSource>();
        }
        objectMusicSource.clip = objectMusicClip;
        objectMusicSource.loop = true;
        objectMusicSource.playOnAwake = false;
    }

    void Update()
    {
        // Verifica la distanza tra il giocatore e l'oggetto
        float distance = Vector3.Distance(transform.position, player.position);
        canInteract = distance <= interactionDistance; // Se il giocatore è abbastanza vicino, può interagire

        // Se il giocatore è vicino all'oggetto e preme "E", alterna tra inizio e fine interazione
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (isInteracting)
            {
                StopInteraction();  // Ferma il voice over
            }
            else
            {
                StartInteraction(); // Avvia l'interazione e il voice over
            }
        }
    }

    void StartInteraction()
    {
        isInteracting = true;

        // Dissolvenza per abbassare la musica d'ambiente
        if (ambientMusicSource != null && ambientMusicSource.isPlaying)
        {
            StartCoroutine(FadeOut(ambientMusicSource, fadeDuration));
        }

        // Avvia la musica dell'oggetto
        if (objectMusicSource != null && objectMusicClip != null)
        {
            objectMusicSource.Play();
        }

        // Riproduce il voice over se disponibile
        if (voiceOverClip != null && voiceOverAudioSource != null)
        {
            voiceOverAudioSource.clip = voiceOverClip;
            voiceOverAudioSource.Play();
        }

        Debug.Log("Inizio analisi oggetto: " + objectDescription);
    }

    void StopInteraction()
    {
        isInteracting = false;

        // Ferma la musica dell'oggetto
        if (objectMusicSource != null && objectMusicSource.isPlaying)
        {
            objectMusicSource.Stop();
        }

        // Dissolvenza per riportare la musica d'ambiente al volume originale
        if (ambientMusicSource != null && ambientMusicClip != null)
        {
            StartCoroutine(FadeIn(ambientMusicSource, fadeDuration));
        }

        // Ferma il voice over
        if (voiceOverAudioSource.isPlaying)
        {
            voiceOverAudioSource.Stop();
        }

        Debug.Log("Fine analisi oggetto.");
    }

    // Coroutine per abbassare gradualmente il volume
    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    // Coroutine per alzare gradualmente il volume
    IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.Play();
        float startVolume = 0f;
        audioSource.volume = startVolume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1, t / duration);
            yield return null;
        }

        audioSource.volume = 1;
    }
}
