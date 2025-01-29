using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // Componente AudioSource assegnato da Unity
    [SerializeField] private AudioClip backgroundMusic; // Clip della musica di sottofondo
    private bool hasStartedMusic = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Se non è assegnato, aggiunge un AudioSource
        }

        // Configura l'AudioSource
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f; // Regola il volume se necessario
    }

    void Update()
    {
        // Controlla se InfermieriaManager.instance esiste e la pastiglia è stata trovata
        if (InfermieriaManager.instance != null && InfermieriaManager.instance.pastigliaTrovata && !hasStartedMusic)
        {
            PlayBackgroundMusic();
            hasStartedMusic = true; // Evita di riavviare la musica più volte
        }
    }

    public void PlayBackgroundMusic()
    {
        if (!audioSource.isPlaying) // Assicura che la musica non parta due volte
        {
            audioSource.Play();
        }
    }
}
