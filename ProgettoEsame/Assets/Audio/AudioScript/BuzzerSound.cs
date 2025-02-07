using UnityEngine;

public class BuzzerSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Riferimento all'AudioSource
    [SerializeField] private AudioClip buzzerClip; // Suono del buzzer

    private void Start()
    {
        // Assicura che l'AudioSource sia presente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayBuzzer()
    {
        if (buzzerClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(buzzerClip);
        }
        else
        {
            Debug.LogWarning("BuzzerSound: AudioSource o AudioClip non assegnato!");
        }
    }
}
