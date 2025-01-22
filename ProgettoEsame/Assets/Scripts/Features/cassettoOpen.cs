using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettoOpen : MonoBehaviour
{
    public Transform cassetto; // Riferimento al cassetto
    public float zPosizioneAperta; // Valore della posizione z del cassetto aperto
    private Vector3 posizioneChiusa; // Posizione del cassetto chiuso
    public float velocitaApertura = 2f; // Velocità di apertura/chiusura
    private bool isOpen = false; // Stato del cassetto
    public float raycastDistance = 5f; // Distanza del raycast
    public LayerMask cassettoLayer; // Layer del cassetto

    // Start is called before the first frame update
    void Start()
    {
        if (cassetto == null)
        {
            cassetto = transform;
        }
        posizioneChiusa = cassetto.localPosition; // Imposta la posizione chiusa come la posizione iniziale del cassetto
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance, cassettoLayer))
            {
                if (hit.transform == transform)
                {
                    isOpen = !isOpen;
                    StopAllCoroutines();
                    StartCoroutine(MuoviCassetto(isOpen ? new Vector3(posizioneChiusa.x, posizioneChiusa.y, zPosizioneAperta) : posizioneChiusa));
                }
            }
        }
    }

    private IEnumerator MuoviCassetto(Vector3 destinazione)
    {
        while (Vector3.Distance(cassetto.localPosition, destinazione) > 0.01f)
        {
            cassetto.localPosition = Vector3.Lerp(cassetto.localPosition, destinazione, Time.deltaTime * velocitaApertura);
            yield return null;
        }
        cassetto.localPosition = destinazione;
    }
} 