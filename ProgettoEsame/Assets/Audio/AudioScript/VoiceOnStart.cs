using System.Collections;
using UnityEngine;

public class VoiceOnStart : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // AudioSource da cui riprodurre il suono
    [SerializeField] private AudioClip voiceClip;  // La clip audio della voce
    [SerializeField] private float delayTime = 2f;  // Il ritardo in secondi prima di far partire l'audio

    void Start()
    {
        // Verifica se l'AudioSource è assegnato
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Imposta il clip audio sull'AudioSource
        audioSource.clip = voiceClip;

        // Avvia la coroutine per far partire il suono dopo il ritardo
        StartCoroutine(PlayVoiceWithDelay());
    }

    // Coroutine che riproduce la voce dopo il ritardo
    private IEnumerator PlayVoiceWithDelay()
    {
        // Attendi il tempo di ritardo
        yield return new WaitForSeconds(delayTime);

        // Riproduce la voce
        audioSource.Play();
    }
}
