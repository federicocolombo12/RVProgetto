using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    // Start is called before the first frame update
    private NpcHeadLookAt npcHeadLookAt;
    [SerializeField] float playerHeight = 0.8f;
    [SerializeField] Transform otherNpc;
    [SerializeField] NpcInteraction playerInteraction;
    bool stopInteract=false;
    
    private void Awake()
    {
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();

    }
    

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interacting with NPC "+ gameObject.name);
        
        npcHeadLookAt.LookAtPosition(interactorTransform.position+ Vector3.up * playerHeight);
        stopInteract = false;
    }
    public void StopInteract()
    {
        stopInteract = true;
        npcHeadLookAt.LookAtPosition(otherNpc.position);
    }
}
