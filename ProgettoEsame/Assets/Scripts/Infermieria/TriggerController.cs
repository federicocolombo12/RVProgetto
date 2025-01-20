using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerLock playerLock;
    private Transform player;
    public Camera mainCamera;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    
    private void Start()
    {
       
        player = mainCamera.transform;

    }
    private void Update()
    {
        InteractWithNurse();
    }


    public void InteractWithNurse()
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
                playerLock.reachedPoint = true;
            }
        }
       
    }
}
