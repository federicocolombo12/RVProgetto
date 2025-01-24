using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paziente0Audio : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip runningClip;
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip walkingClip;
    [SerializeField] private AudioClip talkingClip;

    void Start()
    {
        // Recupera i riferimenti agli altri componenti se non sono stati assegnati manualmente
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
        // Verifica se l'animatore è in un certo stato per determinare l'audio
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
