using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSound : MonoBehaviour
{
    [SerializeField] private AudioClip interactionSound;  // Suono da riprodurre
    [SerializeField] private AudioSource audioSource;     // AudioSource per riprodurre il suono

    void Start()
    {
        
        
            audioSource = GetComponent<AudioSource>();  // Se non è assegnato, prova a cercarlo sul GameObject
        
    }

    // Funzione per riprodurre il suono
    public void PlayInteractionSound()
    {
        
        
            audioSource.PlayOneShot(interactionSound);
        
        
    }
}
