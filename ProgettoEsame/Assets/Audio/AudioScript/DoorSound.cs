using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public AudioSource audioSource;           // L'AudioSource per riprodurre i suoni
    public AudioClip doorOpenClip;            // Clip audio per l'apertura della porta
    public AudioClip doorCloseClip;           // Clip audio per la chiusura della porta
    public float interactionDistance = 3f;    // Distanza massima per interagire con la porta

    private bool isDoorOpen = false;          // Stato della porta (aperta o chiusa)
    private Transform player;                 // Riferimento al giocatore (per il controllo della distanza)
    private Camera playerCamera;              // Riferimento alla fotocamera del giocatore

    void Start()
    {
        player = Camera.main.transform;       // Supponiamo che il giocatore sia il personaggio controllato dalla fotocamera
        playerCamera = Camera.main;           // Riferimento alla fotocamera
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assicurati che l'AudioSource non riproduca i suoni automaticamente
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Update()
    {
        // Verifica se il giocatore è abbastanza vicino per interagire con la porta
        float distance = Vector3.Distance(transform.position, player.position);

        // Se il giocatore è abbastanza vicino e ha cliccato il mouse
        if (distance <= interactionDistance && Input.GetMouseButtonDown(0)) // 0 = clic sinistro del mouse
        {
            ToggleDoor();  // Chiama il metodo per aprire o chiudere la porta
        }
    }

    void ToggleDoor()
    {
        if (isDoorOpen)
        {
            // Se la porta è aperta, la chiudiamo e riproduciamo il suono di chiusura
            CloseDoor();
        }
        else
        {
            // Se la porta è chiusa, la apriamo e riproduciamo il suono di apertura
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        // Riproduci il suono di apertura
        if (doorOpenClip && audioSource)
        {
            audioSource.clip = doorOpenClip;
            audioSource.Play();
        }

        // Cambia lo stato della porta
        isDoorOpen = true;

        // Qui puoi anche aggiungere l'animazione per aprire la porta, se la hai
        // Ad esempio: Animator.SetTrigger("OpenDoor");
    }

    void CloseDoor()
    {
        // Riproduci il suono di chiusura
        if (doorCloseClip && audioSource)
        {
            audioSource.clip = doorCloseClip;
            audioSource.Play();
        }

        // Cambia lo stato della porta
        isDoorOpen = false;

        // Qui puoi anche aggiungere l'animazione per chiudere la porta, se la hai
        // Ad esempio: Animator.SetTrigger("CloseDoor");
    }
}
