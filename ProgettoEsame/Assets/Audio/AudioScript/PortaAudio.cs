using UnityEngine;

public class PortaAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip portaApertaClip; // Suono quando la porta si apre
    [SerializeField] private AudioClip portaChiusaClip; // Suono se la porta è chiusa

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayPortaAperta()
    {
        if (portaApertaClip != null)
        {
            audioSource.PlayOneShot(portaApertaClip);
        }
    }

    public void PlayPortaChiusa()
    {
        if (portaChiusaClip != null)
        {
            audioSource.PlayOneShot(portaChiusaClip);
        }
    }
}
