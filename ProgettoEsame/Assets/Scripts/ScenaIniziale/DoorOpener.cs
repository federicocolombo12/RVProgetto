using System;
using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Animator doorAnimator;
    [SerializeField] private GameObject doorPrefab;
    private Transform player;
    public Camera mainCamera;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;

    // Riferimento allo script audio
    private PortaAudio portaAudio;


    private void Start()
    {
        player = mainCamera.transform;
        portaAudio = GetComponent<PortaAudio>();
    }

    void Update()
    {
        if (FirstSceneManager.instance.doorOpenable)
        {
            Debug.Log("Door is now openable!");
            if (doorPrefab == null)
            {
                
                return;
            }
            
            doorPrefab.transform.GetChild(0).tag = "OggettoInteragibile1";
            doorPrefab.GetComponent<PlayAudioInteract>().enabled = false;
            doorPrefab.GetComponent<AudioSource>().enabled = false;
            DoorActivate();
        }
    }

    private IEnumerator WaitForAnimationStart()
    {
        // Attende che la porta inizi l'animazione
        yield return new WaitUntil(() => FirstSceneManager.instance.startAnimation);
        doorAnimator.SetBool("DoorOpen", true); // Avvia l'animazione di apertura
        //doorPrefab.GetComponent<AudioSource>().Play(); 
        // Riproduce il suono di porta aperta
        if (portaAudio != null)
        {
            portaAudio.PlayPortaAperta();
        }
    }

    private void DoorActivate()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // Se colpisce una porta interagibile
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);

            if (Input.GetKeyDown(KeyCode.E)) // Se preme il tasto "E"
            {
                Debug.Log("Tasto E premuto.");

                if (FirstSceneManager.instance.doorOpenable) // Se la porta è apribile
                {
                    FirstSceneManager.instance.doorOpen = true; // La porta si apre
                     // Suono di porta apribile
                    StartCoroutine(WaitForAnimationStart());
                    
                }
                else
                {
                    // **Se la porta è chiusa, suona il suono della porta bloccata**
                    if (portaAudio != null)
                    {
                        portaAudio.PlayPortaChiusa();
                    }
                }

            }
        }
    }
}
