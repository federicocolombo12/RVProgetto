using UnityEngine;

public class PazienteSdraiato : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // Per riprodurre un suono quando l'interazione avviene
    [SerializeField] private AudioClip interactionClip; // Clip audio dell'interazione

    // Metodo per avviare l'interazione
    public void StartInteraction()
    {
        // Puoi mettere qui la logica per l'interazione, come un suono o un'animazione
        if (audioSource != null && interactionClip != null)
        {
            audioSource.PlayOneShot(interactionClip);  // Riproduce il suono di interazione
        }

        // Altri comportamenti come animazioni o altre azioni
    }
}
