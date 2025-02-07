using System.Collections;
using UnityEngine;

public class NpcSound : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundSound;  // Suono di fondo
    [SerializeField] private AudioClip interactionSound; // Suono di interazione
    [SerializeField] private AudioSource audioSource;    // AudioSource per riprodurre i suoni

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();  // Se non assegnato, trova l'AudioSource
        }

        if (backgroundSound != null)
        {
            PlayBackgroundSound();  // Avvia il suono di fondo all'inizio
        }
    }

    // Avvia il suono di fondo in loop
    private void PlayBackgroundSound()
    {
        audioSource.clip = backgroundSound;
        audioSource.loop = true;  // Imposta il loop
        audioSource.Play();
    }

    // Funzione per riprodurre il suono di interazione
    public void PlayInteractionSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();  // Ferma qualsiasi suono in corso
        }

        StartCoroutine(PlayInteractionThenResumeBackground());
    }

    private IEnumerator PlayInteractionThenResumeBackground()
    {
        audioSource.PlayOneShot(interactionSound); // Riproduce il suono di interazione

        // Aspetta la durata del suono di interazione prima di riavviare il suono di fondo
        yield return new WaitForSeconds(interactionSound.length);

        PlayBackgroundSound();  // Riprende il suono di sottofondo
    }
}
