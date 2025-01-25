using UnityEngine;

public class DrawerSound : MonoBehaviour
{
    public AudioSource audioSource;           // L'AudioSource per riprodurre i suoni
    public AudioClip drawerOpenClip;          // Clip audio per l'apertura
    public AudioClip drawerCloseClip;         // Clip audio per la chiusura

    private void Start()
    {
        // Aggiungi un AudioSource se non è già presente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assicurati che l'AudioSource non riproduca i suoni automaticamente
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    // Metodo per riprodurre il suono di apertura
    public void PlayOpenSound()
    {
        if (drawerOpenClip && audioSource)
        {
            audioSource.clip = drawerOpenClip;
            audioSource.Play();
        }
    }

    // Metodo per riprodurre il suono di chiusura
    public void PlayCloseSound()
    {
        if (drawerCloseClip && audioSource)
        {
            audioSource.clip = drawerCloseClip;
            audioSource.Play();
        }
    }
}
