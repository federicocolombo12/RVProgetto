using System.Collections;
using UnityEngine;

public class Paziente1AudioManager : MonoBehaviour
{
    [Header("Impostazioni del dialogo")]
    public AudioClip[] dialoghi; // Array di clip audio per il dialogo
    public AudioSource audioSource; // AudioSource per riprodurre i dialoghi

    [Tooltip("Intervallo tra le linee di dialogo, in secondi.")]
    public float intervalloTraDialoghi = 2f; // Pausa tra i dialoghi

    private int indiceDialogoCorrente = 0; // Indice del dialogo corrente
    private bool inDialogo = false; // Controllo per evitare sovrapposizioni di dialogo

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    /// <summary>
    /// Avvia il dialogo con il paziente.
    /// </summary>
    public void AvviaDialogo()
    {
        if (!inDialogo && dialoghi.Length > 0)
        {
            StartCoroutine(DialogoRoutine());
        }
    }

    /// <summary>
    /// Coroutine per gestire il dialogo.
    /// </summary>
    private IEnumerator DialogoRoutine()
    {
        inDialogo = true;

        while (indiceDialogoCorrente < dialoghi.Length)
        {
            // Riproduce il dialogo corrente
            audioSource.clip = dialoghi[indiceDialogoCorrente];
            audioSource.Play();

            // Attende la fine del dialogo corrente
            yield return new WaitForSeconds(audioSource.clip.length + intervalloTraDialoghi);

            indiceDialogoCorrente++;
        }

        // Fine del dialogo
        indiceDialogoCorrente = 0; // Reset per eventuali futuri dialoghi
        inDialogo = false;
    }

    /// <summary>
    /// Interrompe il dialogo in corso.
    /// </summary>
    public void InterrompiDialogo()
    {
        StopAllCoroutines();
        audioSource.Stop();
        inDialogo = false;
        indiceDialogoCorrente = 0;
    }
}
