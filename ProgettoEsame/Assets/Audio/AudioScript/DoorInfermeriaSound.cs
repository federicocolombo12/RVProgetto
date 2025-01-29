using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpenSound;  // Suono della porta che si apre
    [SerializeField] private AudioSource audioSource;  // AudioSource per riprodurre il suono

    private Animator animator;  // Riferimento all'Animator della porta

    void Start()
    {
        animator = GetComponent<Animator>();  // Ottieni il riferimento all'Animator
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();  // Se non è assegnato, cerca l'AudioSource
        }
    }

    // Funzione chiamata quando la porta si apre (evento animazione)
    public void PlayDoorOpenSound()
    {
        if (doorOpenSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(doorOpenSound);  // Riproduci il suono quando la porta si apre
        }
    }
}
