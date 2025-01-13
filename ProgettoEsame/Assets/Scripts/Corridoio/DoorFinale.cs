using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinale : MonoBehaviour
{
    public Animator doorAnimator;
    private Transform player;
    public Camera mainCamera;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    private void Start()
    {
        doorAnimator = GameObject.Find("DoppiaPortaPivot").GetComponent<Animator>();
        mainCamera = Camera.main;
        player = mainCamera.transform;


    }

    // Update is called once per frame
    void Update()
    {
        
        
            Debug.Log("Door is now openable!");
            DoorActivate();

        
    }
    private IEnumerator WaitForAnimationStart()
    {
        // Wait until startAnimation becomes true
        yield return new WaitUntil(() => CorridoioManager.instance.startAnimationPorta);

        // Execute the code after startAnimation becomes true
        // Place your code here
        doorAnimator.SetBool("DoubleDoorOpen", true);
    }

    private void DoorActivate()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // ricordati di mettere il layer Interaclable agli oggetti su unity
        {
            


            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto.");
                CorridoioManager.instance.doorOpen = true;
                // Wait until Scene is loaded
                StartCoroutine(WaitForAnimationStart());

            }

        }
    }
}