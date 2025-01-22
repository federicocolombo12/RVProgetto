using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public Camera mainCamera;
    public static DoorController instance;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;

    private Transform player;
    public bool isDoorOpened = false;
    public bool guardOpenDoor = false;

    private void Start()
    {
        player = mainCamera.transform;
    }

    private void Update()
    {
        if (guardOpenDoor && !isDoorOpened)
        {
            OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }

    private void TryOpenDoor()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);

            isDoorOpened = !isDoorOpened;
            doorAnimator.SetBool("DoorOpen", isDoorOpened);

            if (isDoorOpened)
            {
                Debug.Log("La porta è stata aperta.");
            }
            else
            {
                Debug.Log("La porta è stata chiusa.");
            }
        }
    }

    private void OpenDoor()
    {
        isDoorOpened = true;
        doorAnimator.SetBool("DoorOpen", true);
        Debug.Log("La porta è stata aperta dal guardiano.");
    }
}
