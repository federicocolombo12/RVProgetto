using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StanzaFinalePassatoManager : MonoBehaviour
{
    public static StanzaFinalePassatoManager instance { get; private set; }
    [SerializeField] public GameObject oggetto1;
    [SerializeField] public GameObject oggetto2;
    public bool primoOggetto = false;
    public bool ultimoOggetto = false; // Stato del quinto oggetto

    [Header("Oggetto Finale")]
    public GameObject videoObject; // Oggetto che contiene il VideoPlayer

    public GameObject canvasObject; // Aggiungi il riferimento al Canvas

    [Header("Luce Puntiforme")]
    public Light lucePuntiforme; // Riferimento alla luce puntiforme

    [SerializeField] ObjectInteraction triggerScript;
    [SerializeField] public float delayTime = 30f; // Tempo di attesa per avviare il video

    private bool videoDelayStarted = false; // Variabile per evitare di avviare la coroutine più volte

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

        if (lucePuntiforme != null)
        {
            StartCoroutine(LuceRottaCoroutine());
        }
        else
        {
            Debug.LogError("Luce puntiforme non assegnata nel StanzaFinaleManager.");
        }

        if (triggerScript == null)
        {
            triggerScript = FindObjectOfType<ObjectInteraction>();
            if (triggerScript == null)
            {
                Debug.LogError("Nessun componente ObjectInteraction trovato.");
            }
        }
    }

    private void Update()
    {
        if (triggerScript != null && triggerScript.hasActivated && !videoDelayStarted)
        {
            videoDelayStarted = true;
            StartVideoAfterDelay();
        }
    }

    // Metodo per avviare il video
    public void AvviaVideo()
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
            primoOggetto = true;
            Debug.Log("Interagito con Oggetto1.");
        }
        else if (interactedObject == oggetto2)
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

    // Coroutine per gestire l'effetto luce rotta
    private IEnumerator LuceRottaCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10,20)); // Attende 10 secondi

            // Flicker veloce per 1 secondo
            float flickerDuration = 2f;
            float flickerEndTime = Time.time + flickerDuration;
            while (Time.time < flickerEndTime)
            {
                lucePuntiforme.gameObject.SetActive(!lucePuntiforme.gameObject.activeSelf);
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); // Cambia lo stato della luce ogni 0.1 secondi
            }

            // Assicurati che la luce sia accesa alla fine del flicker
            lucePuntiforme.gameObject.SetActive(true);
        }
    }


    // Metodo per avviare il video dopo un ritardo
    public void StartVideoAfterDelay()
    {
        StartCoroutine(WaitAndStartVideo());
    }

    // Coroutine per attendere 30 secondi e poi avviare il video
    private IEnumerator WaitAndStartVideo()
    {
        yield return new WaitForSeconds(delayTime); // Attende 30 secondi
        AvviaVideo(); // Chiama il metodo AvviaVideo
    }
}
