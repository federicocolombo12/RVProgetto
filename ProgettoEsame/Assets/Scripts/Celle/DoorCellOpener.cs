using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator; // Riferimento all'Animator della porta
    public Camera mainCamera;     // Riferimento alla telecamera del giocatore
    public static DoorController instance;

    [SerializeField] private float interactionDistance = 2f; // Distanza massima di interazione
    [SerializeField] private LayerMask interactableLayer;    // Layer degli oggetti interagibili

    private Transform player;     // Riferimento al Transform del giocatore
    public bool isDoorOpened = false; // Stato per controllare se la porta è già stata aperta

    private void Start()
    {
        // Imposta il riferimento al Transform della telecamera principale
        player = mainCamera.transform;
    }

    private void Update()
    {
        // Controlla se il giocatore preme il tasto E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }

    private void TryOpenDoor()
    {
        // Crea un raggio dalla posizione del giocatore in avanti
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        // Controlla se il raggio colpisce un oggetto nel layer interagibile
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);

            // Se la porta non è ancora stata aperta
            if (!isDoorOpened)
            {
                isDoorOpened = true; // Segna che la porta è stata aperta

                // Avvia l'animazione di apertura
                doorAnimator.SetBool("DoorOpen", true);

                Debug.Log("La porta è stata aperta.");
            }
        }
    }
}
