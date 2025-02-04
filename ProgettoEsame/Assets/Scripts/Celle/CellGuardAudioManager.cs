using UnityEngine;
using System.Collections;

public class CellGuardAudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Idle Audio Clips")]
    public AudioClip[] idleClips;
    private int currentIdleIndex = 0; // Tiene traccia della sequenza

    [Header("Walking Audio Clip")]
    public AudioClip walkingClip;

    private bool isPlayingIdle = false; // Per evitare sovrapposizioni

    public void PlayIdle()
    {
        if (idleClips.Length > 0 && !isPlayingIdle)
        {
            isPlayingIdle = true; // Segna che sta suonando
            StartCoroutine(PlayIdleSequence());
        }
    }

    private IEnumerator PlayIdleSequence()
    {
        if (currentIdleIndex >= idleClips.Length) currentIdleIndex = 0; // Riavvolge la sequenza

        AudioClip clipToPlay = idleClips[currentIdleIndex];
        audioSource.PlayOneShot(clipToPlay);

        yield return new WaitForSeconds(clipToPlay.length); // Aspetta che finisca

        isPlayingIdle = false;
        currentIdleIndex++; // Passa alla clip successiva
    }

    public void PlayWalking()
    {
        if (walkingClip != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(walkingClip);
        }
    }
}
