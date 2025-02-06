using System.Collections;
using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    public AudioSource audioSource; // Assegna l'AudioSource dall'Inspector
    public float delay = 60f; // Ritardo in secondi

    void Start()
    {
        StartCoroutine(PlayAudioWithDelay());
    }

    IEnumerator PlayAudioWithDelay()
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource non assegnato!");
        }
    }
}
