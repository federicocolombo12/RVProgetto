using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CellGuardNpc : MonoBehaviour
{
    public float idleTime = 2f; // Tempo in secondi prima che inizi a camminare
    public float secondIdleTime = 5f; // Tempo in secondi per il secondo idle
    public Transform firstDestination; // Prima destinazione
    public Transform secondDestination; // Seconda destinazione
    public Transform thirdDestination; // Terza destinazione
    public Transform fourthDestination; // Quarta destinazione
    public float walkSpeed = 1f; // Velocità di camminata
    public float stoppingDistance = 0.5f; // Distanza di arresto
    public float rightTurnDuration = 1f; // Durata della rotazione a destra

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    public bool OggettoNascosto = false; // Variabile pubblica per controllare lo stato di OggettoNascosto

    // Evento che segnala la fine dell'animazione
    public event Action OnAnimationEnd;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (animator == null)
        {
            Debug.LogError("Animator non trovato sul personaggio!");
            return;
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent non trovato sul personaggio!");
            return;
        }

        navMeshAgent.speed = walkSpeed;
        navMeshAgent.stoppingDistance = stoppingDistance;

        // Inizia la routine di comportamento
        StartCoroutine(GuardRoutine());

        // Trova l'oggetto KnifePlacement e registra l'evento OnKnifePlaced
        KnifePlacement knifePlacement = FindObjectOfType<KnifePlacement>();
        if (knifePlacement != null)
        {
            knifePlacement.OnKnifePlaced += HandleKnifePlaced;
        }
        else
        {
            Debug.LogError("KnifePlacement non trovato nella scena!");
        }
    }

    private void HandleKnifePlaced()
    {
        SetOggettoNascosto(true);
    }

    private IEnumerator GuardRoutine()
    {
        while (true)
        {
            // Stato iniziale: Idle
            Debug.Log("Inizio Idle");
            animator.SetBool("IsWalking", false);
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Passa allo stato di camminata verso la prima destinazione
            Debug.Log("Inizio Camminata verso la prima destinazione");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(firstDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle e rotazione a sinistra
            Debug.Log("Arrivato alla prima destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningLeft", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.SetBool("IsTurningLeft", false);
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(secondIdleTime);

            // Rotazione a destra
            Debug.Log("Inizio Rotazione a Destra");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(rightTurnDuration);
            animator.SetBool("IsTurningRight", false);

            // Passa allo stato di camminata verso la seconda destinazione
            Debug.Log("Inizio Camminata verso la seconda destinazione");
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(secondDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle e rotazione a sinistra
            Debug.Log("Arrivato alla seconda destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningLeft", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.SetBool("IsTurningLeft", false);
            animator.SetBool("SetIdle", true);

            // Attesa fino a quando OggettoNascosto non diventa true
            Debug.Log("In attesa di OggettoNascosto");
            yield return new WaitUntil(() => OggettoNascosto);

            // Rotazione a destra
            Debug.Log("Inizio Rotazione a Destra");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(rightTurnDuration);
            animator.SetBool("IsTurningRight", false);

            // Passa allo stato di camminata verso la terza destinazione
            Debug.Log("Inizio Camminata verso la terza destinazione");
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(thirdDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle e rotazione a sinistra
            Debug.Log("Arrivato alla terza destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningLeft", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.SetBool("IsTurningLeft", false);
            animator.SetBool("SetIdle", true);

            // Passa allo stato di camminata verso la quarta destinazione
            Debug.Log("Inizio Camminata verso la quarta destinazione");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(fourthDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Torna allo stato di idle
            Debug.Log("Arrivato alla quarta destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Segnala la fine dell'animazione
            OnAnimationEnd?.Invoke();
        }
    }

    void Update()
    {
        // Puoi aggiungere eventuali aggiornamenti se necessari
    }

    public void SetOggettoNascosto(bool value)
    {
        OggettoNascosto = value;
        if (OggettoNascosto)
        {
            Debug.Log("OggettoNascosto è diventato true, inizio a camminare verso la quarta destinazione");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(fourthDestination.position);
        }
    }
}
