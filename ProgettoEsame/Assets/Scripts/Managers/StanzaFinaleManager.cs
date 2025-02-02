using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StanzaFinaleManager : MonoBehaviour
{
    public static StanzaFinaleManager instance { get; private set; }

    [Header("Oggetti Interagibili")]
    public GameObject oggetto1;
    public GameObject oggetto2;
    public GameObject oggetto3;
    public GameObject oggetto4;
    public GameObject quintoOggetto; // Il quinto oggetto che avvia il video

    [Header("Stato Interazione")]
    public bool Oggetto1 = false;
    public bool Oggetto2 = false;
    public bool Oggetto3 = false;
    public bool Oggetto4 = false;
    public bool ultimoOggetto = false; // Stato del quinto oggetto

    [Header("Oggetto Finale")]
    public GameObject videoObject; // Oggetto che contiene il VideoPlayer

    public GameObject canvasObject; // Aggiungi il riferimento al Canvas

    [Header("Trigger Animazioni")]
    [SerializeField] private string triggerWalk = "TriggerWalk";
    [SerializeField] private string triggerIdle = "TriggerIdle";
    [SerializeField] private string triggerBackward = "TriggerBackward";

    [SerializeField] private Animator animator; // Riferimento all'Animator

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (videoObject != null && canvasObject != null)
        {
            videoObject.SetActive(false); // Disattiva il video all'inizio
            canvasObject.SetActive(false); // Disattiva anche il Canvas all'inizio
        }
        else
        {
            Debug.LogError("Oggetto video o Canvas non assegnato nel StanzaFinaleManager.");
        }

        if (animator == null)
        {
            Debug.LogError("Nessun componente Animator assegnato.");
        }
        else
        {
            StartCoroutine(TriggerSequence());
        }
    }

    private void Update()
    {
        Debug.Log($"Oggetto1: {Oggetto1}, Oggetto2: {Oggetto2}, Oggetto3: {Oggetto3}, Oggetto4: {Oggetto4}, UltimoOggetto: {ultimoOggetto}");

        // Verifica se tutti gli oggetti sono stati interagiti e se non è stato ancora avviato il video
        if (Oggetto1 && Oggetto2 && Oggetto3 && Oggetto4 && !ultimoOggetto)
        {
            Debug.Log("Tutti gli oggetti sono stati interagiti. Puoi ora interagire con il quinto oggetto.");
        }
    }

    // Metodo per avviare il video
    private void AvviaVideo()
    {
        if (canvasObject != null && videoObject != null && !canvasObject.activeSelf)
        {
            canvasObject.SetActive(true); // Attiva il Canvas che contiene il video
            videoObject.SetActive(true);  // Attiva il GameObject del VideoPlayer (lo rende visibile)

            Debug.Log("Video avviato.");

            VideoPlayer videoPlayer = videoObject.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                // Avvia il video
                videoPlayer.Play();
                Debug.Log("VideoPlayer avviato.");
            }
            else
            {
                Debug.LogError("Nessun componente VideoPlayer trovato sull'oggetto video.");
            }
        }
    }

    // Metodo per controllare se un oggetto è stato interagito
    public void CheckInteraction(GameObject interactedObject)
    {
        if (interactedObject == oggetto1)
        {
            Oggetto1 = true;
            Debug.Log("Interagito con Oggetto1.");
        }
        else if (interactedObject == oggetto2)
        {
            Oggetto2 = true;
            Debug.Log("Interagito con Oggetto2.");
        }
        else if (interactedObject == oggetto3)
        {
            Oggetto3 = true;
            Debug.Log("Interagito con Oggetto3.");
        }
        else if (interactedObject == oggetto4)
        {
            Oggetto4 = true;
            Debug.Log("Interagito con Oggetto4.");
        }
        else if (interactedObject == quintoOggetto)
        {
            ultimoOggetto = true;
            Debug.Log("Interagito con l'ultimo oggetto. Video verrà avviato.");
            AvviaVideo();
        }
        else
        {
            Debug.LogWarning("Oggetto non riconosciuto o interazione non consentita: " + interactedObject.name);
        }
    }

    // Coroutine per attivare i trigger in sequenza
    private IEnumerator TriggerSequence()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger(triggerWalk);
        Debug.Log("TriggerWalk attivato.");

        yield return new WaitForSeconds(10f);
        animator.SetTrigger(triggerIdle);
        Debug.Log("TriggerIdle attivato.");
        /*
        yield return new WaitForSeconds(2f);
        animator.SetTrigger(triggerBackward);
        Debug.Log("TriggerBackward attivato.");
        */
    }
}

