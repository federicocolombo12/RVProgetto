using System.Collections;
using UnityEngine;

public class DoorFinaleAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayDoorOpenSound()
    {
        if (doorOpenSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }
    }

    public void PlayDoorCloseSound()
    {
        if (doorCloseSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(doorCloseSound);
        }
    }
}
