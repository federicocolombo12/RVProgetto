using UnityEngine;

public class PortaUnoAudio : MonoBehaviour
{
    public AudioSource audioSource;   // AudioSource per riprodurre i suoni
    public AudioClip suonoAperta;     // Suono per porta apribile
    public AudioClip suonoChiusa;     // Suono per porta non apribile

    // Metodo per riprodurre il suono di porta aperta
    public void PlayPortaAperta()
    {
        if (audioSource != null && suonoAperta != null)
        {
            audioSource.PlayOneShot(suonoAperta);
        }
    }

    // Metodo per riprodurre il suono di porta chiusa
    public void PlayPortaChiusa()
    {
        if (audioSource != null && suonoChiusa != null)
        {
            audioSource.PlayOneShot(suonoChiusa);
        }
    }
}
