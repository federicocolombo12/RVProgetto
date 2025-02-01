using UnityEngine;

public class InfermieraSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // AudioSource assegnato manualmente o aggiunto automaticamente
    [SerializeField] private AudioClip nurseTalkClip; // Clip audio da riprodurre
    [SerializeField] private AudioClip nurseTalkClip2; // Clip audio da riprodurre

    void Start()
    {
      
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayNurseTalkSound(int clipIndex)
    {
        if (!audioSource.isPlaying && nurseTalkClip != null)
        {
            if (clipIndex == 1)
            {
                audioSource.clip = nurseTalkClip;
            }
            else if (clipIndex == 2)
            {
                audioSource.clip = nurseTalkClip2;
            }
            
            audioSource.Play();
        }
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
