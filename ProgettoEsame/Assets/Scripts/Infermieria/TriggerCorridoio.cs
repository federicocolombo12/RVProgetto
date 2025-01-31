using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCorridoio : MonoBehaviour
{
    private Transform player;
    public Camera mainCamera;
    [SerializeField] private AttivaPorta script;
    [SerializeField] private PlayerLock playerLock;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;

    private void Start()
    {
        player = mainCamera.transform;
        playerLock = GetComponent<PlayerLock>();
    }

    private void Update()
    {
        ObjectFound();
    }

    void ObjectFound()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);
            Debug.Log("Giocatore sta guardando l'oggetto.");

            if (playerLock.reachedPoint)
            {
               

                // Controlla se lo script AttivaPorta è attivo
                if (script.enabled)
                {
                    // Assegna il tag "OggettoInteragibile2" a tutti i figli dell'oggetto colpito
                    AssignTagToChildren(hit.transform, "OggettoInteragibile2");
                    Debug.Log("Tag assegnato: OggettoInteragibile2 a tutti i figli");
                }
            }
        }
    }

    void AssignTagToChildren(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.tag = tag;
            AssignTagToChildren(child, tag); // Ricorsione per assegnare il tag ai figli dei figli
        }
    }
}
