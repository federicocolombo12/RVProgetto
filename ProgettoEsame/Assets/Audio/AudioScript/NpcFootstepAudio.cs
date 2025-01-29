using UnityEngine;

public class NpcFootstepAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private float footstepInterval = 0.5f;
    private bool isWalking = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = footstepClip;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void StartFootsteps()
    {
        if (!isWalking)
        {
            isWalking = true;
            InvokeRepeating(nameof(PlayFootstep), 0f, footstepInterval);
        }
    }

    public void StopFootsteps()
    {
        isWalking = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    private void PlayFootstep()
    {
        if (isWalking && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
