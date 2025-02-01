using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioInteract : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact(GameObject interactor)
    {
        Debug.Log("Interacting with audio");
        audioSource.Play();
    }
    public void StopInteract(GameObject interactor)
    {
        audioSource.Stop();
    }
}
