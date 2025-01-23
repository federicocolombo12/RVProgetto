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

    void Start()
    {
        playerCamera = Camera.main; // Assumiamo che la camera principale sia quella del player
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
            transform.rotation = Quaternion.Lerp(rotazioneIniziale, rotazioneFinale, tempoTrascorso / durataApertura);
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
    }
}
