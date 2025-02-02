using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rileva : MonoBehaviour
{
    /*[SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    private Transform player;

    private void Start()
    {
        player = Camera.main.transform; // Associa la posizione della telecamera del giocatore
    }

    private void Update()
    {
        Rilevatore();
    }

    private void Rilevatore()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto.");
                StanzaFinaleManager.instance.CheckInteraction(hit.transform.gameObject);
            }
        }
    }*/
}
