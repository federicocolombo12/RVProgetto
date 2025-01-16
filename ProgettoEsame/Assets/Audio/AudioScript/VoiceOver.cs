using UnityEngine;

public class VoiceOverObject : MonoBehaviour
{
    public AudioSource voiceOverAudioSource;       // L'AudioSource per riprodurre il voice over
    public AudioSource ambientMusicSource;         // L'AudioSource per la musica d'ambiente
    public AudioSource objectMusicSource;          // L'AudioSource per la musica di sottofondo dell'oggetto
    public AudioClip voiceOverClip;                // Clip audio del voice over quando si interagisce con l'oggetto
    public string objectDescription;               // Descrizione opzionale dell'oggetto
    public float interactionDistance = 3f;         // Distanza massima per interagire con l'oggetto

    private Transform player;                      // Riferimento al giocatore
    private bool isInteracting = false;            // Stato di interazione con l'oggetto
    private bool canInteract = false;              // Verifica se il giocatore può interagire con l'oggetto

    void Start()
    {
        player = Camera.main.transform;            // Supponiamo che la fotocamera sia controllata dal giocatore
        if (voiceOverAudioSource == null)
        {
            voiceOverAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configurazione iniziale dell'AudioSource per il voice over
        voiceOverAudioSource.playOnAwake = false;

        // Configurazione iniziale delle musiche
        if (ambientMusicSource != null)
        {
            ambientMusicSource.loop = true;
            ambientMusicSource.Play();             // Avvia la musica d'ambiente
        }

        if (objectMusicSource != null)
        {
            objectMusicSource.loop = true;
            objectMusicSource.playOnAwake = false; // La musica dell'oggetto non parte finché non interagiamo
        }
    }

    void Update()
    {
        // Verifica la distanza tra il giocatore e l'oggetto
        float distance = Vector3.Distance(transform.position, player.position);
        canInteract = distance <= interactionDistance; // Se il giocatore è abbastanza vicino, può interagire

        // Se il giocatore è vicino all'oggetto e preme "E", inizia l'interazione
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            StartInteraction();  // Avvia l'interazione e il voice over
        }

        // Se il giocatore preme "ESC" durante l'interazione, ferma il voice over
        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            StopInteraction();  // Ferma il voice over
        }
    }

    void StartInteraction()
    {
        isInteracting = true;

        // Ferma la musica d'ambiente
        if (ambientMusicSource != null && ambientMusicSource.isPlaying)
        {
            ambientMusicSource.Stop();
        }

        // Avvia la musica dell'oggetto
        if (objectMusicSource != null)
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

        // Riprendi la musica d'ambiente
        if (ambientMusicSource != null)
        {
            ambientMusicSource.Play();
        }

        // Ferma il voice over
        if (voiceOverAudioSource.isPlaying)
        {
            voiceOverAudioSource.Stop();
        }

        Debug.Log("Fine analisi oggetto.");
    }
}
