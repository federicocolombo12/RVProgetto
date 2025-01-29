using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazienteSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip talkClip; // Suono della voce del paziente

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayTalkSound()
    {
        if (talkClip != null && !audioSource.isPlaying)
        {
            Debug.Log("Riproduzione suono paziente.");
            audioSource.clip = talkClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip mancante o già in riproduzione!");
        }
    }
}
