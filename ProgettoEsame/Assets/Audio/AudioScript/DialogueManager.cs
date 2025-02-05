using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource; // Riferimento all'AudioSource, assegnabile tramite Inspector
    public AudioClip startQueueAudio; // Clip audio da far partire all'inizio della fila

    private void Awake()
    {
        // Verifica che l'AudioSource sia assegnato
        if (audioSource == null)
        {
            Debug.LogError("AudioSource non assegnato in DialogueManager! Assegna un AudioSource nell'Inspector.");
        }
    }

    public void PlayStartQueueAudio()
    {
        // Verifica che l'AudioSource e la clip audio siano validi
        if (audioSource != null && startQueueAudio != null)
        {
            audioSource.PlayOneShot(startQueueAudio);
        }
        else
        {
            if (startQueueAudio == null)
            {
                Debug.LogWarning("Start queue audio clip non assegnato!");
            }
        }
    }
}
