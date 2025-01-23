using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGuardAudio : MonoBehaviour
{
    [SerializeField] CellGuardNpc script;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip runningClip;
    [SerializeField] AudioClip idleClip;
    [SerializeField] AudioClip walkingClip;
    [SerializeField] AudioClip talkingClip;

    void Start()
    {
        script = GetComponent<CellGuardNpc>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ManageAudioStates();
    }

    void ManageAudioStates()
    {
        if (script.audioRunning && audioSource.clip != runningClip)
        {
            PlayAudioClip(runningClip);
        }
        else if (script.audioIdle && audioSource.clip != idleClip)
        {
            PlayAudioClip(idleClip);
        }
        else if (script.audioWalking && audioSource.clip != walkingClip)
        {
            PlayAudioClip(walkingClip);
        }
        else if (script.audioTalking && audioSource.clip != talkingClip)
        {
            PlayAudioClip(talkingClip);
        }
        else if (!script.audioRunning && !script.audioIdle && !script.audioWalking && !script.audioTalking)
        {
            StopAudio();
        }
    }

    void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}
