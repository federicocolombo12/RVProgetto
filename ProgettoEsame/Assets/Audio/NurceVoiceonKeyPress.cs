using UnityEngine;

public class NurseVoiceOnKeyPress : MonoBehaviour
{
    public AudioSource nurseVoice; // Riferimento all'AudioSource della voce dell'infermiera
    private bool hasPlayed = false; // Per evitare che la voce venga riprodotta più volte

    void Update()
    {
        // Controlla se il tasto E è stato premuto e la voce non è già partita
        if (Input.GetKeyDown(KeyCode.E) && !hasPlayed)
        {
            PlayNurseVoice();
        }
    }

    private void PlayNurseVoice()
    {
        if (nurseVoice != null)
        {
            nurseVoice.Play();
            hasPlayed = true; // Imposta a true per evitare ripetizioni
        }
    }
}
