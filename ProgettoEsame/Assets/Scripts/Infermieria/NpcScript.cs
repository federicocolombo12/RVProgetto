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
    bool stopInteract = false;
    public NpcState currentState = NpcState.Idle;

    [SerializeField] private NpcSound npcSound;  // Aggiungi il riferimento allo script NpcSound

    public enum NpcState
    {
        Idle,
        Interacting
    }

    private void Awake()
    {
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();
    }

    void Update()
    {
        switch (currentState)
        {
            case NpcState.Idle:
                if (!stopInteract)
                {
                    StopInteract();
                }
                break;

            case NpcState.Interacting:
                if (stopInteract)
                {
                    Interact(Camera.main.transform);
                }
                break;
        }
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interacting with NPC " + gameObject.name);

        // Riproduci il suono dell'interazione
        if (npcSound != null)
        {
            npcSound.PlayInteractionSound();  // Chiamata al metodo per riprodurre il suono
        }

        npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
        stopInteract = false;
    }

    public void StopInteract()
    {
        stopInteract = true;
        npcHeadLookAt.LookAtPosition(otherNpc.position);
    }
}
