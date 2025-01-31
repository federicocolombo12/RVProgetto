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
    }

    private void Update()
    {
      
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
}
