using UnityEngine;

public class InfermieraBlockingAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // AudioSource assegnato manualmente o aggiunto automaticamente
    [SerializeField] private AudioClip firstInteractionClip; // Clip audio per la prima interazione
    [SerializeField] private AudioClip subsequentInteractionClip; // Clip audio per le interazioni successive
    private InfermieraBlocking infermieraBlocking;
    private int interactionCount = 0; // Conta le interazioni

    void Start()
    {
        // Assicuriamoci che l'AudioSource sia presente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configura l'AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Trova il componente InfermieraBlocking
        infermieraBlocking = GetComponent<InfermieraBlocking>();

        if (infermieraBlocking == null)
        {
            Debug.LogError("InfermieraBlocking non trovato su " + gameObject.name + ". Assicurati che sia presente nello stesso GameObject.");
        }
    }

    // Questo metodo viene chiamato quando inizia l'interazione con l'infermiera
    public void PlayNurseTalk()
    {
        if (!audioSource.isPlaying) // Evita di riprodurre il suono se già in corso
        {
            // Controlla se è la prima interazione o una successiva
            if (interactionCount == 0)
            {
                // Prima interazione
                audioSource.clip = firstInteractionClip;
            }
            else
            {
                // Interazioni successive
                audioSource.clip = subsequentInteractionClip;
            }

            audioSource.Play(); // Riproduci il clip
            interactionCount++; // Incrementa il contatore delle interazioni
        }
    }

    // Questo metodo viene chiamato quando termina l'interazione con l'infermiera
    public void StopNurseTalk()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
