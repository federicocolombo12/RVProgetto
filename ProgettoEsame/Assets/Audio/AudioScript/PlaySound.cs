using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private Raccolta_Vedi_Oggetto raccolta_Vedi_Oggetto_script; // Riferimento allo script Raccolta_Vedi_Oggetto
    [SerializeField] private AudioSource audioSource; // Riferimento alla sorgente audio
    private bool hasPlayed = false; // Indica se l'audio è già stato riprodotto

    void Start()
    {
        // Ottieni i componenti necessari, se non già assegnati nell'Inspector
        if (raccolta_Vedi_Oggetto_script == null)
        {
            raccolta_Vedi_Oggetto_script = GetComponent<Raccolta_Vedi_Oggetto>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Controlla se il booleano `playAudio` è true
        if (raccolta_Vedi_Oggetto_script != null && raccolta_Vedi_Oggetto_script.playAudio)
        {
            if (!hasPlayed) // Verifica se l'audio non è già stato riprodotto
            {
                audioSource.Play(); // Riproduce l'audio
                hasPlayed = true; // Imposta lo stato come "già riprodotto"
            }
        }
        else
        {
            // Ripristina lo stato se `playAudio` è false
            audioSource.Stop();
            if (hasPlayed)
            {
               
                hasPlayed = false;
            }
        }
    }
}

