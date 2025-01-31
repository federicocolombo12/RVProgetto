using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlashbackObject : MonoBehaviour
{
    private Transform player;
    public Camera mainCamera;
    
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField]   private LayerMask interactableLayer;
    
    // Start is called before the first frame update
    private void Start()
    {
        
        player = mainCamera.transform;
        
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

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // ricordati di mettere il layer Interaclable agli oggetti su unity
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);
                
            
            Debug.Log("Giocatore sta guardando l'oggetto.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto.");
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("FirstObject"))                {
                    CorridoioManager.instance.firstObjectFound = true;
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("SecondObject"))
                {
                    CorridoioManager.instance.secondObjectFound = true;
                }
            }
        }
        
    }
    public void TriggerFlashbackInfermieria()
    {
        CorridoioManager.instance.firstObjectFound = true;
    }
    public void TriggerFlashbackCelle()
    {
        CorridoioManager.instance.secondObjectFound = true;
    }
}
