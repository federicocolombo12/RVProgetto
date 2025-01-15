using UnityEngine;

public class VoiceOverObject : MonoBehaviour
{
    public AudioSource audioSource;           // L'AudioSource per riprodurre il voice over
    public AudioClip voiceOverClip;           // Clip audio del voice over quando si prende l'oggetto
    public string objectDescription;          // Descrizione opzionale dell'oggetto
    public float interactionDistance = 3f;    // Distanza massima per interagire con l'oggetto

    private Transform player;                 // Riferimento al giocatore
    private bool isInteracting = false;       // Stato di interazione con l'oggetto
    private bool canInteract = false;         // Verifica se il giocatore può interagire con l'oggetto (es. vicino a un libro)

    void Start()
    {
        player = Camera.main.transform;       // Supponiamo che la fotocamera sia controllata dal giocatore
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assicurati che l'AudioSource non riproduca il voice over all'avvio
        audioSource.playOnAwake = false;
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
        if (voiceOverClip != null && audioSource != null)
        {
            isInteracting = true;
            audioSource.clip = voiceOverClip;
            audioSource.Play();  // Riproduce il voice over
            Debug.Log("Inizio analisi oggetto: " + objectDescription);
        }
    }

    void StopInteraction()
    {
        isInteracting = false;

        // Ferma la riproduzione del voice over
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        Debug.Log("Fine analisi oggetto.");
    }
}
