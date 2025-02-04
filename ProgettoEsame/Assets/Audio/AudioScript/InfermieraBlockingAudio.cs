using UnityEngine;

public class InfermieraBlockingAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // AudioSource assegnato manualmente o aggiunto automaticamente
    [SerializeField] private AudioClip firstInteractionClip; // Clip audio per la prima interazione
    [SerializeField] private AudioClip subsequentInteractionClip; // Clip audio per le interazioni successive
    [SerializeField] private AudioClip nurseWalkingClip; // Clip audio per la camminata dell'infermiera

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

    // Metodo per riprodurre il suono della camminata dell'infermiera
    public void PlayNurseWalking()
    {
        if (!audioSource.isPlaying || audioSource.clip != nurseWalkingClip)
        {
            audioSource.clip = nurseWalkingClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Metodo per fermare il suono della camminata
    public void StopNurseWalking()
    {
        if (audioSource.isPlaying && audioSource.clip == nurseWalkingClip)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
}