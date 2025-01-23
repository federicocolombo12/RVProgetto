using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paziente0Audio : MonoBehaviour
{
    [SerializeField] Paziente0Script script;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip runningClip;
    [SerializeField] AudioClip idleClip;
    [SerializeField] AudioClip walkingClip;
    [SerializeField] AudioClip talkingClip;

    void Start()
    {
        script = GetComponent<Paziente0Script>();
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
        else if (script.audioidle && audioSource.clip != idleClip)
        {
            PlayAudioClip(idleClip);
        }
        else if (script.audiowalking && audioSource.clip != walkingClip)
        {
            PlayAudioClip(walkingClip);
        }
        else if (script.audiotalking && audioSource.clip != talkingClip)
        {
            PlayAudioClip(talkingClip);
        }
        else if (!script.audioRunning && !script.audioidle && !script.audiowalking && !script.audiotalking)
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
