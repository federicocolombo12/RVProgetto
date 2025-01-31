using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfermieraInteract : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public void Interact(GameObject player)
    {
        Debug.Log("Interacting with Infermiera");

    
    }
    public void StopInteract(GameObject player) { 
        throw new System.NotImplementedException();
    }

}
