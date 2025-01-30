using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazienteSound : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip talkClip; // Suono della voce del paziente
    bool interacted = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        talkClip = audioSource.clip;
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("Interazione con il paziente.");
        if (!interacted)
        {
            interacted = true;
            if (talkClip != null && !audioSource.isPlaying)
            {
                Debug.Log("Riproduzione suono paziente.");
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioClip mancante o già in riproduzione!");
            }
        }
        
        
    }
    public void StopInteract(GameObject interactor)
    {
        interacted = false;
        audioSource.Stop();
    }
}
