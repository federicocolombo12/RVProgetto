using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paziente2ScriptNPC : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator non trovato sul Paziente 2!");
        }
    }

    void Update()
    {
        if (NpcCelleInteractionManager.instance.paziente2Interaction)
        {
            Interagisci();
            NpcCelleInteractionManager.instance.paziente2Interaction = false; 
        }
    }

    private void Interagisci()
    {
        if (animator != null)
        {
            animator.SetTrigger("Interagisci");
        }
    }
}
