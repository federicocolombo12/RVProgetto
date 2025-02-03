using UnityEngine;

public class TorciaAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip accensioneClip; // Suono accensione torcia
    [SerializeField] private AudioClip spegnimentoClip; // Suono spegnimento torcia
    [SerializeField] private float pitchMin = 0.8f; // Valore minimo del pitch
    [SerializeField] private float pitchMax = 0.9f; // Valore massimo del pitch

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayAccensioneSound()
    {
        if (audioSource != null && accensioneClip != null)
        {
            audioSource.pitch = Random.Range(pitchMin, pitchMax); // Imposta un pitch più basso casuale
            audioSource.PlayOneShot(accensioneClip);
        }
    }

    public void PlaySpegnimentoSound()
    {
        if (audioSource != null && spegnimentoClip != null)
        {
            audioSource.pitch = Random.Range(pitchMin, pitchMax); // Imposta un pitch più basso casuale
            audioSource.PlayOneShot(spegnimentoClip);
        }
    }
}
