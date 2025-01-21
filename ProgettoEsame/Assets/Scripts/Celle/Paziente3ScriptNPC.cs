using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paziente3ScriptNPC : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator non trovato sul Paziente 3!");
        }
    }

    void Update()
    {
        if (NpcCelleInteractionManager.instance.paziente3Interaction)
        {
            Interagisci();
            NpcCelleInteractionManager.instance.paziente3Interaction = false;
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
