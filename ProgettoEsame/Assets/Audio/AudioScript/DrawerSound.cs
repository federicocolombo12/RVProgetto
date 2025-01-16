using UnityEngine;

public class DrawerSound : MonoBehaviour
{
    public AudioSource audioSource;           // L'AudioSource per riprodurre i suoni
    public AudioClip drawerOpenClip;          // Clip audio per l'apertura
    public AudioClip drawerCloseClip;         // Clip audio per la chiusura
    public float interactionDistance = 3f;    // Distanza massima per interagire con il cassetto

    private bool isDrawerOpen = false;        // Stato del cassetto (aperto o chiuso)
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
        // Verifica se il giocatore è abbastanza vicino per interagire con il cassetto
        float distance = Vector3.Distance(transform.position, player.position);

        // Se il giocatore è abbastanza vicino e ha cliccato il mouse
        if (distance <= interactionDistance && Input.GetMouseButtonDown(0)) // 0 = clic sinistro del mouse
        {
            ToggleDrawer();  // Chiama il metodo per aprire o chiudere il cassetto
        }
    }

    void ToggleDrawer()
    {
        if (isDrawerOpen)
        {
            // Se il cassetto è aperto, lo chiudiamo e riproduciamo il suono di chiusura
            CloseDrawer();
        }
        else
        {
            // Se il cassetto è chiuso, lo apriamo e riproduciamo il suono di apertura
            OpenDrawer();
        }
    }

    void OpenDrawer()
    {
        // Riproduci il suono di apertura
        if (drawerOpenClip && audioSource)
        {
            audioSource.clip = drawerOpenClip;
            audioSource.Play();
        }

        // Cambia lo stato del cassetto
        isDrawerOpen = true;

        // Qui puoi anche aggiungere l'animazione per aprire il cassetto, se la hai
        // Ad esempio: Animator.SetTrigger("OpenDrawer");
    }

    void CloseDrawer()
    {
        // Riproduci il suono di chiusura
        if (drawerCloseClip && audioSource)
        {
            audioSource.clip = drawerCloseClip;
            audioSource.Play();
        }

        // Cambia lo stato del cassetto
        isDrawerOpen = false;

        // Qui puoi anche aggiungere l'animazione per chiudere il cassetto, se la hai
        // Ad esempio: Animator.SetTrigger("CloseDrawer");
    }
}
