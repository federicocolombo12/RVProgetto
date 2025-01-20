using UnityEngine;

public class SpeakWithNurse : MonoBehaviour
{
    public string dialogo = "Ciao! Come posso aiutarti?"; // Il dialogo che l'infermiera dirà
    public float distanzaInterazione = 3f; // Distanza di interazione con l'infermiera
    public KeyCode tastoInterazione = KeyCode.E; // Tasto per interagire (ad esempio 'E')

    public AudioClip clipDialogo; // Clip audio che verrà riprodotta quando si interagisce
    private AudioSource audioSource; // AudioSource per riprodurre la clip audio

    private Transform giocatore; // Riferimento al giocatore
    private bool inZonaInterazione = false; // Se il giocatore è nella zona di interazione

    // Start viene chiamato prima del primo frame
    void Start()
    {
        giocatore = Camera.main.transform; // Il giocatore è la fotocamera principale
        audioSource = GetComponent<AudioSource>(); // Ottieni il componente AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Aggiungi un AudioSource se non c'è
        }
    }

    // Update viene chiamato una volta per frame
    void Update()
    {
        // Calcola la distanza tra l'infermiera e il giocatore
        float distanza = Vector3.Distance(transform.position, giocatore.position);

        // Controlla se il giocatore è nella zona di interazione
        if (distanza <= distanzaInterazione)
        {
            inZonaInterazione = true;
            // Mostra un messaggio che puoi interagire (puoi aggiungere un prompt sullo schermo se lo desideri)
            Debug.Log("Premi E per parlare con l'infermiera.");
        }
        else
        {
            inZonaInterazione = false;
        }

        // Se il giocatore è vicino e preme il tasto di interazione, il dialogo viene attivato
        if (inZonaInterazione && Input.GetKeyDown(tastoInterazione))
        {
            Speak(); // Chiamato quando il giocatore preme il tasto
        }
    }

    // Funzione che attiva il dialogo
    void Speak()
    {
        // Qui puoi fare qualsiasi cosa, ad esempio mostrare il dialogo
        Debug.Log(dialogo); // Mostra il testo nella console (puoi cambiarlo per una UI)

        // Riproduci la clip audio se è stata assegnata
        if (clipDialogo != null)
        {
            audioSource.PlayOneShot(clipDialogo);
        }
    }
}
