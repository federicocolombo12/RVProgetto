using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Animator doorAnimator;
    private Transform player;
    public Camera mainCamera;
    public Animator doppiaPortaAnimatorSinistra;
    public Animator doppiaPortaAnimatorDestra;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask doubleDoorLayer; // Nuovo LayerMask per la nuova porta doppia
    private bool doubleDoorOpen = false; // Nuovo bool per gestire lo stato della nuova porta doppia

    private void Start()
    {
        player = mainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstSceneManager.instance.doorOpenable)
        {
            Debug.Log("Door is now openable!");
            DoorActivate();
        }
    }

    private void DoorActivate()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // ricordati di mettere il layer Interaclable agli oggetti su unity
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);
            Debug.Log("Giocatore sta guardando l'oggetto.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto.");
                FirstSceneManager.instance.doorOpen = true;
                // Imposta il parametro dell'animator per aprire la porta
                doorAnimator.SetBool("DoorOpen", true);
            }
        }
        else if (Physics.Raycast(ray, out hit, interactionDistance, doubleDoorLayer)) // Gestione del nuovo layer
        {
            Debug.Log("Raycast ha colpito la nuova porta doppia: " + hit.transform.name);
            Debug.Log("Giocatore sta guardando la nuova porta doppia.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto per la nuova porta doppia.");
                doubleDoorOpen = true;
                CorridoioManager.instance.doorOpen = true; // Aggiungi questa riga
                // Imposta i parametri degli animator per aprire le porte doppie
                doppiaPortaAnimatorSinistra.SetBool("DoorOpen", true);
                doppiaPortaAnimatorDestra.SetBool("DoorOpen", true);
            }
        }
    }
}

