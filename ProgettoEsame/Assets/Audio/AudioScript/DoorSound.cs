using UnityEngine;

public class DoorSoundOnFadeOut : MonoBehaviour
{
    public AudioSource audioSource;           // L'AudioSource per riprodurre il suono della porta
    public AudioClip doorSoundClip;           // Clip audio per il suono della porta

    private bool hasPlayedSound = false;      // Flag per evitare che il suono venga riprodotto più di una volta

    void Start()
    {
        // Assicurati che l'AudioSource non sia null
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Controlliamo se il FadeOut è stato avviato nel TransitionScript
        if (TransitionScript.instance != null && !hasPlayedSound)
        {
            // Se il FadeOut è iniziato (vuoi un modo per sapere se è attivo)
            if (TransitionScript.instance.IsFadingOut(0))
            {
                PlayDoorSound();
            }
        }
    }

    // Metodo per riprodurre il suono
    private void PlayDoorSound()
    {
        // Assicurati che il suono venga riprodotto solo una volta
        if (doorSoundClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(doorSoundClip);
            hasPlayedSound = true; // Segna che il suono è stato già riprodotto
        }
    }
}

