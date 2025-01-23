using UnityEngine;

public class PauseSoundManager : MonoBehaviour
{
    [Header("Suoni per il menu di pausa")]
    public AudioClip suonoAttivazioneMenu; // Suono da riprodurre quando si attiva il menu
    public AudioClip suonoDisattivazioneMenu; // Suono da riprodurre quando si disattiva il menu

    [Tooltip("AudioSource personalizzato per il suono di attivazione. Se lasciato vuoto, ne verrà creato uno automaticamente.")]
    public AudioSource audioSourceAttivazione; // AudioSource per il suono di attivazione

    [Tooltip("AudioSource personalizzato per il suono di disattivazione. Se lasciato vuoto, ne verrà creato uno automaticamente.")]
    public AudioSource audioSourceDisattivazione; // AudioSource per il suono di disattivazione

    public bool riproduciSuonoAttivazione = true; // Booleano per abilitare/disabilitare il suono di attivazione
    public bool riproduciSuonoDisattivazione = true; // Booleano per abilitare/disabilitare il suono di disattivazione

    private void Awake()
    {
        // Se l'AudioSource di attivazione non è stato assegnato, creane uno
        if (audioSourceAttivazione == null)
        {
            audioSourceAttivazione = gameObject.AddComponent<AudioSource>();
        }

        // Se l'AudioSource di disattivazione non è stato assegnato, creane uno
        if (audioSourceDisattivazione == null)
        {
            audioSourceDisattivazione = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Metodo per riprodurre entrambi i suoni (attivazione e disattivazione) contemporaneamente
    /// </summary>
    public void RiproduciSuoni()
    {
        // Riproduce il suono di attivazione se è abilitato
        if (riproduciSuonoAttivazione && suonoAttivazioneMenu != null && audioSourceAttivazione != null)
        {
            audioSourceAttivazione.PlayOneShot(suonoAttivazioneMenu);
        }

        // Riproduce il suono di disattivazione se è abilitato
        if (riproduciSuonoDisattivazione && suonoDisattivazioneMenu != null && audioSourceDisattivazione != null)
        {
            audioSourceDisattivazione.PlayOneShot(suonoDisattivazioneMenu);
        }
    }

    /// <summary>
    /// Metodo per fermare entrambi i suoni (attivazione e disattivazione) contemporaneamente
    /// </summary>
    public void FermaSuoni()
    {
        // Ferma il suono di attivazione se è in esecuzione
        audioSourceAttivazione.Stop();

        // Ferma il suono di disattivazione se è in esecuzione
        audioSourceDisattivazione.Stop();
    }

    /// <summary>
    /// Metodo per fermare i suoni quando si esce dal menu (ad esempio, cambiando scena)
    /// </summary>
    public void FermaSuoniQuandoEsci()
    {
        // Ferma entrambi i suoni quando esci dal menu
        FermaSuoni();
    }
}
