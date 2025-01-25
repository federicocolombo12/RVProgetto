using System.Collections;
using UnityEngine;

public class CassettoOpen : MonoBehaviour
{
    public Transform cassetto;               // Riferimento al cassetto
    public float zPosizioneAperta;           // Valore della posizione z del cassetto aperto
    private Vector3 posizioneChiusa;         // Posizione del cassetto chiuso
    public float velocitaApertura = 2f;      // Velocità di apertura
    public float velocitaChiusura = 3f;      // Velocità di chiusura (modificata per aumentarla)
    private bool isOpen = false;             // Stato del cassetto
    public float raycastDistance = 5f;       // Distanza del raycast
    public LayerMask cassettoLayer;          // Layer del cassetto
    public DrawerSound drawerSound;          // Riferimento allo script DrawerSound

    void Start()
    {
        if (cassetto == null)
        {
            cassetto = transform;
        }

        posizioneChiusa = cassetto.localPosition; // Imposta la posizione chiusa come la posizione iniziale del cassetto

        // Se drawerSound non è stato assegnato nell'Inspector, cerca nel GameObject corrente o nei figli
        if (drawerSound == null)
        {
            drawerSound = GetComponentInChildren<DrawerSound>();
        }

        if (drawerSound == null)
        {
            Debug.LogError($"DrawerSound non trovato su {gameObject.name}! Assicurati di assegnarlo nell'Inspector o che sia presente come componente figlio.");
        }
    }

    void Update()
    {
        // Controlla se il tasto "E" viene premuto e il raycast colpisce il cassetto
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance, cassettoLayer))
            {
                if (hit.transform == transform)
                {
                    ToggleDrawer(); // Apri o chiudi il cassetto
                }
            }
        }
    }

    void ToggleDrawer()
    {
        isOpen = !isOpen; // Cambia lo stato del cassetto

        // Ferma eventuali coroutines in corso e avvia il movimento del cassetto
        StopAllCoroutines();
        StartCoroutine(MuoviCassetto(isOpen ? new Vector3(posizioneChiusa.x, posizioneChiusa.y, zPosizioneAperta) : posizioneChiusa));

        // Riproduci il suono corrispondente
        if (drawerSound != null)
        {
            if (isOpen)
            {
                drawerSound.PlayOpenSound();
            }
            else
            {
                drawerSound.PlayCloseSound();
            }
        }
    }

    private IEnumerator MuoviCassetto(Vector3 destinazione)
    {
        // Se stiamo chiudendo il cassetto, usa la velocità di chiusura
        float velocita = isOpen ? velocitaApertura : velocitaChiusura;

        while (Vector3.Distance(cassetto.localPosition, destinazione) > 0.01f)
        {
            cassetto.localPosition = Vector3.Lerp(cassetto.localPosition, destinazione, Time.deltaTime * velocita);
            yield return null;
        }
        cassetto.localPosition = destinazione;
    }
}
