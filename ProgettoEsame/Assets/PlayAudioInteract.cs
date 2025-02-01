using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioInteract : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] AudioSource audioSource;
    bool played = false;
    int counter = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact(GameObject interactor)
    {
        Debug.Log("Interacting with " + gameObject.name);
        if (!audioSource.isPlaying && !played && counter == 0)
        {
            // Cambia il tag dell'oggetto corrente
            gameObject.tag = "Untagged";

            // Cambia il tag di tutti i figli
            ChangeTagOfChildren(gameObject, "Untagged");

            audioSource.PlayOneShot(audioSource.clip);

            played = true;
            counter++;
        }
    }
    public void StopInteract(GameObject interactor)
    {
        played = false;
    }

    // Metodo per cambiare il tag di tutti i figli
    void ChangeTagOfChildren(GameObject parent, string newTag)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.tag = newTag;
            // Ricorsivamente cambia il tag dei figli dei figli
            ChangeTagOfChildren(child.gameObject, newTag);
        }
    }
}
