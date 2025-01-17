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
    public string testoInterazioneOggetto1 = "E"; // Testo per il primo tipo di oggetto
    public string testoInterazioneOggetto2 = "F"; // Testo per il secondo tipo di oggetto
    private Transform oggettoInteragibile; // Riferimento all'oggetto interagibile
    private Camera playerCamera; // Riferimento alla camera del giocatore
    public LayerMask interactableLayer; // Layer per gli oggetti interagibili
    public float rayDistance = 5f; // Distanza massima del raggio
    private bool inVisualizzazione = false; // Stato della modalità di visualizzazione

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

        interazioneTesto.text = ""; // Inizialmente il testo è vuoto
        interazioneTesto.gameObject.SetActive(false); // Nascondi il testo all'inizio
        interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo all'inizio
        uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita all'inizio
        uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita all'inizio
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
        if (playerCamera == null)
        {
            Debug.LogError("Camera principale non trovata.");
            return;
        }

        if (inVisualizzazione)
        {
            // Mostra il testo e l'immagine di sfondo per l'uscita in basso a destra
            uscitaTesto.text = "E"; // Testo per uscire
            uscitaTesto.gameObject.SetActive(true); // Mostra il testo di uscita
            uscitaSfondo.gameObject.SetActive(true); // Mostra l'immagine di sfondo per l'uscita

            if (Input.GetKeyDown(KeyCode.E))
            {
                inVisualizzazione = false;
                uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita
                uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita
            }
            return;
        }

        // Controlla se il giocatore sta guardando un oggetto interagibile
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);
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
                    interazioneTesto.gameObject.SetActive(false); // Nascondi il testo di interazione
                    interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo di interazione
                }

                Debug.Log("Testo interazione mostrato per OggettoInteragibile1.");
            }
            else if (hit.transform.CompareTag("OggettoInteragibile2"))
            {
                oggettoInteragibile = hit.transform;

                // Mostra il testo e l'immagine di sfondo per il secondo tipo di oggetto
                interazioneTesto.text = testoInterazioneOggetto2; // Mostra il testo per il secondo tipo di oggetto
                interazioneTesto.gameObject.SetActive(true); // Mostra il testo
                interazioneSfondo.gameObject.SetActive(true); // Mostra l'immagine di sfondo

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inVisualizzazione = true;
                    interazioneTesto.gameObject.SetActive(false); // Nascondi il testo di interazione
                    interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo di interazione
                }

                Debug.Log("Testo interazione mostrato per OggettoInteragibile2.");
            }
            else
            {
                interazioneTesto.gameObject.SetActive(false); // Nascondi il testo quando il giocatore non guarda un oggetto interagibile
                interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo
                uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita
                uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita
                Debug.Log("Il giocatore non sta guardando un oggetto interagibile.");
            }
        }
        else
        {
            interazioneTesto.gameObject.SetActive(false); // Nascondi il testo quando il giocatore non guarda l'oggetto
            interazioneSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo
            uscitaTesto.gameObject.SetActive(false); // Nascondi il testo di uscita
            uscitaSfondo.gameObject.SetActive(false); // Nascondi l'immagine di sfondo per l'uscita
            Debug.Log("Raycast non ha colpito nulla.");
        }
    }
}