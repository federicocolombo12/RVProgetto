using UnityEngine;

public class CassettoBloccato : MonoBehaviour
{
    public AudioClip drawerCloseClip;   // Clip audio per il cassetto chiuso
    private AudioSource audioSource;    // Sorgente audio

    public float interactionDistance = 0.5f;  // Distanza massima per interagire

    private Transform player;          // Riferimento al giocatore (per la distanza)

    void Start()
    {
        // Ottieni il componente AudioSource, se non è già assegnato
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Se non c'è, aggiungilo
        }

        // Configura l'AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Riferimento al giocatore
        player = Camera.main.transform;
    }

    void Update()
    {
        // Verifica la distanza tra il giocatore e il cassetto
        float distance = Vector3.Distance(transform.position, player.position);

        // Se il giocatore è abbastanza vicino e ha premuto il tasto E
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            PlayCloseSound();  // Riproduce il suono di chiusura
        }
    }

    void PlayCloseSound()
    {
        // Riproduce il suono di cassetto chiuso
        if (drawerCloseClip != null && audioSource != null)
        {
            audioSource.clip = drawerCloseClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Clip audio non assegnata per il cassetto chiuso!");
        }
    }
}
