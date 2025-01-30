using UnityEngine;

public class TorciaAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip accensioneClip; // Suono accensione
    [SerializeField] private AudioClip spegnimentoClip; // Suono spegnimento

    private Torcia torcia;

    void Start()
    {
        torcia = GetComponent<Torcia>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayAccensioneSound()
    {
        if (accensioneClip != null)
        {
            audioSource.PlayOneShot(accensioneClip);
        }
    }

    public void PlaySpegnimentoSound()
    {
        if (spegnimentoClip != null)
        {
            audioSource.PlayOneShot(spegnimentoClip);
        }
    }
}
