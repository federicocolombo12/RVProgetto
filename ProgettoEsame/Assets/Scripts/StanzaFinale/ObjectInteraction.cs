using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ObjectInteraction : MonoBehaviour
{
    public Animator animator; // Assegna l'Animator dell'oggetto animato
    public bool hasActivated = false;
    private bool isColliding = false; // Variabile per tenere traccia della collisione
    //public GameObject videoObject; // Oggetto che contiene il VideoPlayer
    //public GameObject canvasObject; // Aggiungi il riferimento al Canva
    public float delayTime = 2f; // Tempo di attesa per avviare il video
    public bool startVideo = false;
/*
    private void Start()
    {
        if (videoObject != null && canvasObject != null)
        {
            videoObject.SetActive(false); // Disattiva il video all'inizio
            canvasObject.SetActive(false); // Disattiva anche il Canvas all'inizio
        }
    }
*/
    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E) && !hasActivated)
        {
            StartCoroutine(TriggerDisapprova());
        }
        else if (isColliding && Input.GetKeyDown(KeyCode.E) && hasActivated)
        {
            StartCoroutine(TriggerActivateAction());
            //StartVideoAfterDelay();
        }
    }

    private IEnumerator TriggerDisapprova()
    {
        animator.SetTrigger("Disapprova");
        yield return new WaitForSeconds(1f); // Aspetta prima di tornare indietro
        animator.SetTrigger("TornaIndietro");
        animator.ResetTrigger("Disapprova");
        hasActivated = true;
    }

    private IEnumerator TriggerActivateAction()
    {
        animator.SetTrigger("ActivateAction");
        hasActivated = false;
        startVideo = true;
        yield return null; // Puoi cambiare se serve una pausa
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assicurati che l'altro oggetto abbia il tag "Player"
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Assicurati che l'altro oggetto abbia il tag "Player"
        {
            isColliding = false;
        }
    }
    /*
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
    private void StartVideoAfterDelay()
    {
        StartCoroutine(WaitAndStartVideo());
    }

    // Coroutine per attendere 30 secondi e poi avviare il video
    private IEnumerator WaitAndStartVideo()
    {
        yield return new WaitForSeconds(delayTime); // Attende 30 secondi
        AvviaVideo(); // Chiama il metodo AvviaVideo
    }*/
}

