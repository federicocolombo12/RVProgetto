using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paziente1ScriptNPC : MonoBehaviour
{
    private Animator animator;
    private NpcHeadLookAtCelle npcHeadLookAtCelle;
    private Paziente3AudioManager audioManager;
    private NavMeshAgent navMeshAgent;
    public Transform PosizionePaziente1; // Assicurati di assegnare questa posizione nel tuo inspector
    [SerializeField] public bool shouldMoveToPosition = false; // Flag per controllare la routine di movimento

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
            Debug.LogError("Animator non trovato sul Paziente 1!");
        }
        npcHeadLookAtCelle = GetComponent<NpcHeadLookAtCelle>();
    }

    void Update()
    {
        if (shouldMoveToPosition)
        {
            StartCoroutine(MoveToPositionRoutine());
            shouldMoveToPosition = false; // Resetta il flag dopo aver avviato la routine
        }

        if (NpcCelleInteractionManager.instance.paziente1Interaction)
        {
            Interagisci();
            NpcCelleInteractionManager.instance.paziente1Interaction = false;
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
        navMeshAgent.SetDestination(PosizionePaziente1.position);

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

    // Metodo per impostare il flag
    public void SetShouldMoveToPosition(bool value)
    {
        shouldMoveToPosition = value;
    }
}

