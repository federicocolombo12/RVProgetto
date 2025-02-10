using UnityEngine;

public class BuzzerSound : MonoBehaviour
{
    public AudioSource audioSource1; // Primo AudioSource
    public AudioSource audioSource2; // Secondo AudioSource

    public AudioClip buzzerClip1; // Primo suono
    public AudioClip buzzerClip2; // Secondo suono

    private void Start()
    {
        // Assicura che entrambi gli AudioSource siano presenti
        if (audioSource1 == null)
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
        }
        if (audioSource2 == null)
        {
            audioSource2 = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayBuzzer1()
    {
        if (buzzerClip1 != null && audioSource1 != null)
        {
            audioSource1.PlayOneShot(buzzerClip1);
        }
        else
        {
            Debug.LogWarning("BuzzerSound: AudioSource1 o AudioClip1 non assegnato!");
        }
    }

    public void PlayBuzzer2()
    {
        if (buzzerClip2 != null && audioSource2 != null)
        {
            audioSource2.PlayOneShot(buzzerClip2);
        }
        else
        {
            Debug.LogWarning("BuzzerSound: AudioSource2 o AudioClip2 non assegnato!");
        }
    }
}
