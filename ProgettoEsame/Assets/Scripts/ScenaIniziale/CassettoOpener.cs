using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettoOpener : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator cassettoAnimator1;
    public Animator cassettoAnimator2;
    private Transform player;
    public Camera mainCamera;
    public AttivaUi attivaUi;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer1;
    [SerializeField] private LayerMask interactableLayer2;
    void Start()
    {
        player = mainCamera.transform;
        attivaUi = FindObjectOfType<AttivaUi>();
    }

    // Update is called once per frame
    void Update()
    {
        TriggerCassetto();
    }
    void TriggerCassetto()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer1)) // ricordati di mettere il layer Interaclable agli oggetti su unity
        {



            attivaUi.Vedi();
            attivaUi.vedi = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto.");
                
                cassettoAnimator1.SetBool("Aperta", !cassettoAnimator1.GetBool("Aperta"));

            }
        }
        else {             
            attivaUi.vedi = false;
        }
        /*if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer2)) // ricordati di mettere il layer Interaclable agli oggetti su unity
        {



            attivaUi.Vedi();
            attivaUi.vedi = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto.");

                cassettoAnimator2.SetBool("Aperta", !cassettoAnimator2.GetBool("Aperta"));
            }
        }
        else
        {
            attivaUi.vedi = false;
        }*/
    }
}
