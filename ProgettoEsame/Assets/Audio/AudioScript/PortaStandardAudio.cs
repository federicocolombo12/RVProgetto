using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaStandardAudio : MonoBehaviour
{
    public AudioSource audioSource; // Componente AudioSource
    public AudioClip aperturaSuono; // Suono di apertura della porta

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayAperturaSound()
    {
        if (audioSource != null && aperturaSuono != null)
        {
            audioSource.PlayOneShot(aperturaSuono);
        }
    }
}
