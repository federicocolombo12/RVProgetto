using UnityEngine;

public class FootstepSoundRigidbody : MonoBehaviour
{
    public AudioSource footstepAudioSource;  // L'audio source per riprodurre il suono dei passi
    public AudioClip footstepClip;           // Clip audio dei passi
    public float stepInterval = 0.5f;       // Intervallo tra i suoni dei passi (in secondi)
    public float movementThreshold = 0.1f;  // Velocità minima per considerare il movimento

    private Rigidbody playerRigidbody;
    private float nextStepTime = 0f;

    void Start()
    {
        // Ottieni il componente Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();

        // Aggiungi l'AudioSource se non è stato assegnato
        if (footstepAudioSource == null)
        {
            footstepAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // Impostazioni dell'AudioSource: disabilitiamo il loop per evitare che suoni continuamente
        footstepAudioSource.loop = false;
        footstepAudioSource.playOnAwake = false;
    }

    void Update()
    {
        // Controlla il movimento del giocatore
        CheckMovement();
    }

    void CheckMovement()
    {
        // Ottieni la velocità attuale del Rigidbody
        Vector3 velocity = playerRigidbody.velocity;

        // Se il giocatore si sta muovendo e la velocità è superiore alla soglia
        if (velocity.magnitude > movementThreshold)
        {
            // Se il suono non è già in riproduzione, lo riproduciamo
            if (!footstepAudioSource.isPlaying && Time.time >= nextStepTime)
            {
                PlayFootstepSound();
                nextStepTime = Time.time + stepInterval;  // Imposta il prossimo passo
            }
        }
        else
        {
            // Se il giocatore è fermo, fermiamo il suono
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
        }
    }

    void PlayFootstepSound()
    {
        // Riproduce il suono dei passi solo se il suono non è già in riproduzione
        footstepAudioSource.clip = footstepClip;
        footstepAudioSource.Play();
    }
}
