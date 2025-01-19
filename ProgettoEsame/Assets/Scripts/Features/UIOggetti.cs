using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIOggetti : MonoBehaviour
{
    public TMP_Text interazioneTesto; // Riferimento al testo UI di TextMesh Pro
    public Image interazioneSfondo; // Riferimento all'immagine di sfondo
    public TMP_Text uscitaTesto; // Riferimento al testo di uscita
    public Image uscitaSfondo; // Riferimento all'immagine di sfondo per l'uscita
    public Image immagineProssimita; // Riferimento all'immagine di prossimità
    public string testoInterazioneOggetto1 = "E"; // Testo per il primo tipo di oggetto
    public string testoInterazioneOggetto2 = "F"; // Testo per il secondo tipo di oggetto
    private Transform oggettoInteragibile; // Riferimento all'oggetto interagibile
    private Camera playerCamera; // Riferimento alla camera del giocatore
    public LayerMask interactableLayer; // Layer per gli oggetti interagibili
    public float rayDistance = 2f; // Distanza massima del raggio
    public float proximityDistance = 3f; // Distanza di prossimità
    private bool inVisualizzazione = false; // Stato della modalità di visualizzazione
    private Transform oggettoInVisualizzazione; // Riferimento all'oggetto attualmente in visualizzazione

    // Start is called before the first frame update
    void Start()
    {
        if (interazioneTesto == null)
        {
            Debug.LogError("interazioneTesto non è assegnato nel Inspector.");
            return;
        }

        if (interazioneSfondo == null)
        {
            Debug.LogError("interazioneSfondo non è assegnato nel Inspector.");
            return;
        }

        if (uscitaTesto == null)
        {
            Debug.LogError("uscitaTesto non è assegnato nel Inspector.");
            return;
        }

        if (uscitaSfondo == null)
        {
            Debug.LogError("uscitaSfondo non è assegnato nel Inspector.");
            return;
        }

        if (immagineProssimita == null)
        {
            Debug.LogError("immagineProssimita non è assegnato nel Inspector.");
            return;
        }

        interazioneTesto.text = ""; // Inizialmente il testo è vuoto
        interazioneTesto.gameObject.SetActive(false); // Nascondi il testo all'inizio
        interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo all'inizio
        uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita all'inizio
        uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita all'inizio
        immagineProssimita.gameObject.SetActive(false); // Nascondi l'immagine di prossimità all'inizio
        playerCamera = Camera.main; // Assumi che la camera principale sia quella del giocatore
        if (playerCamera == null)
        {
            Debug.LogError("Camera principale non trovata.");
        }
        else
        {
            Debug.Log("Start: Camera principale trovata: " + playerCamera.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inVisualizzazione)
        {
            uscitaTesto.text = "E"; // Testo per uscire
            uscitaTesto.gameObject.SetActive(true); // Mostra il testo di uscita
            uscitaSfondo.gameObject.SetActive(true); // Mostra l'immagine di sfondo per l'uscita

            if (Input.GetKeyDown(KeyCode.E))
            {
                inVisualizzazione = false;
                uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita
                uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita
                oggettoInVisualizzazione = null; // Resetta l'oggetto in visualizzazione
            }
            return;
        }

        // Controlla se il giocatore sta guardando un oggetto interagibile
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            immagineProssimita.gameObject.SetActive(false); // Nascondi l'immagine di prossimità

            if (hit.transform.CompareTag("OggettoInteragibile1"))
            {
                oggettoInteragibile = hit.transform;

                // Mostra il testo e l'immagine di sfondo per il primo tipo di oggetto
                interazioneTesto.text = testoInterazioneOggetto1; // Mostra il testo per il primo tipo di oggetto
                interazioneTesto.gameObject.SetActive(true); // Mostra il testo
                interazioneSfondo.gameObject.SetActive(true); // Mostra l'immagine di sfondo

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inVisualizzazione = true;
                    oggettoInVisualizzazione = oggettoInteragibile; // Imposta l'oggetto in visualizzazione
                    interazioneTesto.gameObject.SetActive(false); // Nascondi il testo di interazione
                    interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo di interazione
                }
            }
            else
            {
                interazioneTesto.gameObject.SetActive(false); // Nascondi il testo quando il giocatore non guarda un oggetto interagibile
                interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo
                uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita
                uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita
            }
        }
        else
        {
            interazioneTesto.gameObject.SetActive(false); // Nascondi il testo quando il giocatore non guarda l'oggetto
            interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo
            uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita
            uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita

            // Controlla la distanza per mostrare l'immagine di prossimità
            Collider[] colliders = Physics.OverlapSphere(playerCamera.transform.position, proximityDistance, interactableLayer);
            if (colliders.Length > 0)
            {
                Transform nearestObject = colliders[0].transform;
                float minDistance = Vector3.Distance(playerCamera.transform.position, nearestObject.position);

                foreach (Collider collider in colliders)
                {
                    float distance = Vector3.Distance(playerCamera.transform.position, collider.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestObject = collider.transform;
                    }
                }

                Vector3 screenPosition = playerCamera.WorldToScreenPoint(nearestObject.position);
                immagineProssimita.transform.position = screenPosition;
                immagineProssimita.gameObject.SetActive(true); // Mostra l'immagine di prossimità
            }
            else
            {
                immagineProssimita.gameObject.SetActive(false); // Nascondi l'immagine di prossimità
            }
        }
    }
}