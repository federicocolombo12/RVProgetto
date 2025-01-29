using UnityEngine;

public class InfermieraSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // AudioSource assegnato manualmente o aggiunto automaticamente
    [SerializeField] private AudioClip nurseTalkClip; // Clip audio da riprodurre

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayNurseTalkSound()
    {
        if (!audioSource.isPlaying && nurseTalkClip != null)
        {
            audioSource.clip = nurseTalkClip;
            audioSource.Play();
        }
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
