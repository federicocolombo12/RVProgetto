using System.Collections;
using UnityEngine;

public class DelayedAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Assegna l'AudioSource dall'Inspector
    public float delay = 5f; // Ritardo in secondi

    void Start()
    {
        if (audioSource != null)
        {
            StartCoroutine(PlayAudioWithDelay());
        }
        else
        {
            Debug.LogError("AudioSource non assegnato!");
        }
    }

    IEnumerator PlayAudioWithDelay()
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
