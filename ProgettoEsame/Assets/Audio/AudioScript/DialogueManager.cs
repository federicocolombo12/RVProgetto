using System.Collections; // Necessario per IEnumerator
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource; // L'AudioSource per riprodurre il suono del dialogo
    public AudioClip startDialogueClip; // Clip audio per l'inizio del dialogo
    public AudioClip moveDialogueClip; // Clip audio per il dialogo mentre l'NPC si muove
    public AudioClip endDialogueClip; // Clip audio per la fine del dialogo

    private bool isAudioPlaying = false; // Variabile per controllare se l'audio è già in riproduzione

    public void StartDialogue(string dialogue)
    {
        // Mostra il dialogo o inizia la sequenza
        Debug.Log("Dialogo in corso: " + dialogue);

        // Riproduce il clip audio relativo all'inizio del dialogo solo se non è già in riproduzione
        if (!isAudioPlaying && audioSource != null && startDialogueClip != null)
        {
            isAudioPlaying = true;
            audioSource.PlayOneShot(startDialogueClip);
            // Imposta isAudioPlaying su false quando l'audio è terminato
            StartCoroutine(ResetAudioFlag(startDialogueClip.length));
        }
    }

    public void PlayMovementDialogue()
    {
        // Riproduce il clip audio relativo al movimento solo se non è già in riproduzione
        if (!isAudioPlaying && audioSource != null && moveDialogueClip != null)
        {
            isAudioPlaying = true;
            audioSource.PlayOneShot(moveDialogueClip);
            StartCoroutine(ResetAudioFlag(moveDialogueClip.length));
        }
    }

    public void EndDialogue()
    {
        // Termina il dialogo
        Debug.Log("Dialogo terminato");

        // Riproduce il clip audio relativo alla fine del dialogo solo se non è già in riproduzione
        if (!isAudioPlaying && audioSource != null && endDialogueClip != null)
        {
            isAudioPlaying = true;
            audioSource.PlayOneShot(endDialogueClip);
            StartCoroutine(ResetAudioFlag(endDialogueClip.length));
        }
    }

    // Coroutine per resettare il flag quando l'audio finisce
    private IEnumerator ResetAudioFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAudioPlaying = false;
    }
}
