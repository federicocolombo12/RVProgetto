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
        if (!audioSource.isPlaying && !played&&counter==0)
        {
            gameObject.tag ="Untagged";
            audioSource.PlayOneShot(audioSource.clip);

            played = true;
            counter++;
        }
        
        
       
        
           
    }
    public void StopInteract(GameObject interactor)
    {
        played = false;
    }
    
   
}
