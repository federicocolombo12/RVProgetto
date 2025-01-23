using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AperturaPorta : MonoBehaviour
{
    public float angoloApertura = 120f; // Angolo di apertura della porta
    public float durataApertura = 2f; // Durata dell'animazione di apertura
    public float distanzaInterazione = 3f; // Distanza massima per l'interazione
    [SerializeField] private bool isOpen = false;
    private Quaternion rotazioneIniziale;
    private Quaternion rotazioneFinale;
    private float tempoTrascorso = 0f;
    private Camera playerCamera;

    // Audio
    public AudioClip suonoApertura; // Clip audio per l'apertura della porta
    public AudioClip suonoChiusura; // Clip audio per la chiusura della porta
    public AudioSource audioSource; // Riferimento al componente AudioSource

    void Start()
    {
        playerCamera = Camera.main; // Assumiamo che la camera principale sia quella del player
        rotazioneIniziale = transform.rotation;
        rotazioneFinale = transform.rotation;

        // Controllo se esiste un AudioSource sull'oggetto
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource non assegnato o mancante! Aggiungilo all'oggetto o assegnalo dall'Inspector.");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Premi 'E' per aprire/chiudere la porta
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, distanzaInterazione))
            {
                if (hit.transform == transform)
                {
                    TogglePorta();
                }
            }
        }

        if (tempoTrascorso < durataApertura)
        {
            tempoTrascorso += Time.deltaTime;
            float t = Mathf.Clamp01(tempoTrascorso / durataApertura);
            transform.rotation = Quaternion.Lerp(rotazioneIniziale, rotazioneFinale, t);
        }
    }

    public void ApriPorta()
    {
        if (!isOpen)
        {
            TogglePorta();
        }
    }

    private void TogglePorta()
    {
        isOpen = !isOpen;
        tempoTrascorso = 0f;
        rotazioneIniziale = transform.rotation;
        rotazioneFinale = Quaternion.Euler(transform.eulerAngles + new Vector3(0, isOpen ? angoloApertura : -angoloApertura, 0));

        // Riproduci il suono appropriato
        if (audioSource != null)
        {
            audioSource.clip = isOpen ? suonoApertura : suonoChiusura;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource non assegnato! Nessun suono sarà riprodotto.");
        }
    }
}

