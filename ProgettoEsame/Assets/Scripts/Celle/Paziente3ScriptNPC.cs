using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paziente3ScriptNPC : MonoBehaviour
{
    private Animator animator;
    private NpcHeadLookAtCelle npcHeadLookAtCelle;
    private Paziente3AudioManager audioManager;
    private NavMeshAgent navMeshAgent;
    public Transform PosizionePaziente3; // Assicurati di assegnare questa posizione nel tuo inspector

    void Awake()
    {
        npcHeadLookAtCelle = GetComponent<NpcHeadLookAtCelle>();
        audioManager = GetComponent<Paziente3AudioManager>(); // Recupera il componente audio
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator non trovato sul Paziente 3!");
        }
        npcHeadLookAtCelle = GetComponent<NpcHeadLookAtCelle>();
        StartCoroutine(MoveToPositionRoutine());
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
            StartCoroutine(InteragisciRoutine());
        }

        if (audioManager != null)
        {
            audioManager.AvviaDialogo(); // Avvia il dialogo audio
        }
    }

    private IEnumerator MoveToPositionRoutine()
    {
        // Parte in idle
        Debug.Log("Inizio Camminata verso la prima destinazione");
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalking", true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(PosizionePaziente3.position);

        // Attendi fino a raggiungere la posizione
        yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

        // Ferma il cammino e torna in idle
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
        yield return new WaitForSeconds(2f); // Tempo in idle
    }

    private IEnumerator InteragisciRoutine()
    {
        animator.SetBool("Interagisci", true);
        yield return new WaitForSeconds(4.5f);
        animator.SetBool("Interagisci", false);
        NpcCelleInteractionManager.instance.ReturnToInitialPosition(transform);
    }

    private void PazienteLook()
    {
        npcHeadLookAtCelle.LookAtPosition(Camera.main.transform.position);
    }
}
