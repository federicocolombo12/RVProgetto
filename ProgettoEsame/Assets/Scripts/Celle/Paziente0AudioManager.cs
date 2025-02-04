using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Paziente0AudioManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip runningClip;
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip walkingClip;
    [SerializeField] private AudioClip talkingClip;
    [SerializeField] private AudioClip knifePickupAudioClip; // Nuovo clip audio per il pick-up del coltello

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        ManageAudio();
    }

    private void ManageAudio()
    {
        if (animator.GetBool("IsRunning") && audioSource.clip != runningClip)
        {
            PlayAudioClip(runningClip);
        }
        else if (animator.GetBool("SetIdle") && audioSource.clip != idleClip)
        {
            PlayAudioClip(idleClip);
        }
        else if (animator.GetBool("IsWalking") && audioSource.clip != walkingClip)
        {
            PlayAudioClip(walkingClip);
        }
        else if (animator.GetBool("IsYelling") && audioSource.clip != talkingClip)
        {
            PlayAudioClip(talkingClip);
        }
        else if (!animator.GetBool("IsRunning") && !animator.GetBool("SetIdle") &&
                 !animator.GetBool("IsWalking") && !animator.GetBool("IsYelling"))
        {
            StopAudio();
        }

        // Se il coltello è stato preso, riproduci il nuovo suono
        if (CellaManager.instance.coltelloPreso && audioSource.clip != knifePickupAudioClip)
        {
            PlayAudioClip(knifePickupAudioClip);
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}
