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
    
    private void Awake()
    {
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();

    }
    private void Update()
    {
        if (!playerInteraction.isInteracting)
        {
            StopInteract();
        }
    
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interacting with NPC "+ gameObject.name);
        
        npcHeadLookAt.LookAtPosition(interactorTransform.position+ Vector3.up * playerHeight);
    }
    public void StopInteract()
    {
        npcHeadLookAt.LookAtPosition(otherNpc.position);
    }
}
