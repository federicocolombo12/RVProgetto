using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paziente2ScriptNPC : MonoBehaviour
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
            Debug.LogError("Animator non trovato sul Paziente 2!");
        }
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();
    }

    void Update()
    {
        if (NpcCelleInteractionManager.instance.paziente2Interaction)
        {
            Interagisci();
            NpcCelleInteractionManager.instance.paziente2Interaction = false;
            CellaManager.instance.attivaPazienteRoutine = true;
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
            StartCoroutine(InteragisciRoutine());
        }
    }

    private IEnumerator InteragisciRoutine()
    {
        animator.SetBool("Interagisci", true);
        yield return new WaitForSeconds(4f);
        animator.SetBool("Interagisci", false);
        NpcCelleInteractionManager.instance.ReturnToInitialPosition(transform);
    }

    private void PazienteLook()
    {
        npcHeadLookAt.LookAtPosition(Camera.main.transform.position);
    }
}
