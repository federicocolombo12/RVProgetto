using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    private NpcHeadLookAt npcHeadLookAt;
    [SerializeField] float playerHeight = 0.8f;
    [SerializeField] Transform otherNpc;
    [SerializeField] NpcInteraction playerInteraction;
    [SerializeField] Transform target;
    [SerializeField] private float transitionDuration = 1.0f; // Durata della transizione
    private bool entered = false;
    private bool stopInteract = false;
    public NpcState currentState = NpcState.Idle;
    public NpcState previousState = NpcState.Idle;
    private float transitionTimer = 0.0f;

    [SerializeField] private NpcSound npcSound;

    public enum NpcState
    {
        Idle,
        Transitioning,
        Interacting
    }

    private void Awake()
    {
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();
        npcSound = GetComponent<NpcSound>();
    }

    void Update()
    {
        switch (currentState)
        {
            case NpcState.Idle:
                StopInteract();
                previousState = NpcState.Idle;
                break;

            case NpcState.Transitioning:
                transitionTimer += Time.deltaTime;
                
                if (transitionTimer >= transitionDuration)
                {
                    transitionTimer = 0;
                    if (previousState == NpcState.Idle)
                    {currentState = NpcState.Interacting;

                        Interact(Camera.main.transform);
                    }
                    else
                    {
                        StopInteract();
                    }
                }
                break;

            case NpcState.Interacting:
                npcHeadLookAt.LookAtPosition(Camera.main.transform.position + Vector3.up * playerHeight);
                previousState = NpcState.Interacting;
                break;
        }
    }

    public void StartInteraction()
    {
        
        
            transitionTimer = 0;
            currentState = NpcState.Transitioning;
        
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interacting with NPC " + gameObject.name);

        if (npcSound != null)
        {
            Debug.Log("Playing interaction sound");
            npcSound.PlayInteractionSound();
        }

        npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
        stopInteract = false;
    }

    public void StopInteract()
    {
        stopInteract = true;
        npcHeadLookAt.LookAtPosition(otherNpc.position);
        currentState = NpcState.Idle;
    }
}
