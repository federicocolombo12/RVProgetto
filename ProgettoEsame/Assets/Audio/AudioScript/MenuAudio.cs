using UnityEngine;

public class MenuSound : MonoBehaviour
{
    // Riferimento all'AudioSource per il suono del menu
    public AudioSource menuAudioSource;

    // Riferimento all'AudioClip per il suono del menu
    public AudioClip menuSoundClip;

    // Riferimento all'AudioSource per la musica di background
    public AudioSource backgroundMusicSource;

    // Riferimento all'AudioClip per la musica di background
    public AudioClip backgroundMusicClip;

    // Stato per tenere traccia della riproduzione della musica di background
    private bool isMusicPlaying = false;

    void Start()
    {
        // Assicurati che la musica di background sia inizialmente ferma
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true; // Configura la musica per ripetersi
            backgroundMusicSource.Stop();
        }
    }

    void Update()
    {
        // Controlla se il tasto Esc viene premuto
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Riproduci il suono del menu
            if (menuAudioSource != null && menuSoundClip != null)
            {
                menuAudioSource.PlayOneShot(menuSoundClip);
            }
            else
            {
                Debug.LogWarning("MenuAudioSource o MenuSoundClip non assegnati.");
            }

            // Gestisci la musica di background
            if (backgroundMusicSource != null && backgroundMusicClip != null)
            {
                if (isMusicPlaying)
                {
                    // Ferma la musica di background
                    backgroundMusicSource.Stop();
                }
                else
                {
                    // Avvia la musica di background
                    backgroundMusicSource.Play();
                }

                // Cambia lo stato della musica
                isMusicPlaying = !isMusicPlaying;
            }
            else
            {
                Debug.LogWarning("BackgroundMusicSource o BackgroundMusicClip non assegnati.");
            }
        }
    }
}
