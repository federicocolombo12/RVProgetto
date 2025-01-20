using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNurse1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InfermieraBlocking infermieraBlocking;
    private Transform player;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    public AttivaUi attivaUi;
    void Start()
    {
        player = Camera.main.transform;
        attivaUi = FindObjectOfType<AttivaUi>();
    }

    // Update is called once per frame
    void Update()
    {
        TalkWithNurse();

    }
    void TalkWithNurse()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // ricordati di mettere il layer Interaclable agli oggetti su unity
        {


            attivaUi.Vedi();
            attivaUi.vedi = true;
            if (Input.GetKeyDown(KeyCode.E))
            {


                // Wait until Scene is loaded
                infermieraBlocking.NurseTalk();

            }

        }
        else
        {
            attivaUi.vedi = false;
        }
    }
    
}
