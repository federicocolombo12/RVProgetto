using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenerStandard : MonoBehaviour
{
    public Animator doorAnimator;
    private Transform player;
    public Camera mainCamera;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    private void Start()
    {

        player = mainCamera.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (InfermieriaManager.instance.pastigliaTrovata)
        {
            
            DoorActivate();

        }
    }
    private IEnumerator WaitForAnimationStart()
    {
        

        // Execute the code after startAnimation becomes true
        // Place your code here
        doorAnimator.SetBool("DoorOpen", true);
        yield return new WaitUntil(() => doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Apertura"));
        if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Apertura"))
        {
            InfermieriaManager.instance.isInRow = true;
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
            


            
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                
                // Wait until Scene is loaded
                StartCoroutine(WaitForAnimationStart());

            }

        }
    }
}
