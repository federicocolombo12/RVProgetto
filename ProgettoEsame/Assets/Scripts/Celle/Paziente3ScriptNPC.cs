using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paziente3ScriptNPC : MonoBehaviour
{
    private Animator animator;
    private NpcHeadLookAt npcHeadLookAt;

    void Awake()
    {
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator non trovato sul Paziente 3!");
        }
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();
    }

    void Update()
    {
        if (NpcCelleInteractionManager.instance.paziente3Interaction)
        {
            Interagisci();
            NpcCelleInteractionManager.instance.paziente3Interaction = false;
        }
        else
        {
            PazienteLook();
        }
    }

    private void Interagisci()
    {
        if (animator != null)
        {
            animator.SetTrigger("Interagisci");
        }
    }

    private void PazienteLook()
    {
        npcHeadLookAt.LookAtPosition(Camera.main.transform.position);
    }
}
