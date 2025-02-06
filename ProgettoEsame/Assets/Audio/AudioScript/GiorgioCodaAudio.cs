using UnityEngine;

public class GiorgioCodaAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip firstSound;
    public AudioClip secondSound;
    private bool hasActivated = false;

    public void PlayInteractionSound()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource non assegnato a " + gameObject.name);
            return;
        }

        if (!hasActivated)
        {
            audioSource.clip = firstSound;
            hasActivated = true;
        }
        else
        {
            audioSource.clip = secondSound;
        }

        audioSource.Play();
    }
}
