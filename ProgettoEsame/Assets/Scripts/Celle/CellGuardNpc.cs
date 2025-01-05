using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CellGuardNpc : MonoBehaviour
{
    public float idleTime = 2f; // Tempo in secondi prima che inizi a camminare
    public float secondIdleTime = 5f; // Tempo in secondi per il secondo idle
    public Transform firstDestination; // Prima destinazione
    public Transform secondDestination; // Seconda destinazione
    public float walkSpeed = 1f; // Velocità di camminata
    public float stoppingDistance = 0.5f; // Distanza di arresto
    public float rightTurnDuration = 1f; // Durata della rotazione a destra

    private Animator animator;
    private NavMeshAgent navMeshAgent;

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
            yield return new WaitForSeconds(secondIdleTime);

            // Torna allo stato di idle
            Debug.Log("Torna a Idle");
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Rotazione a destra
            Debug.Log("Inizio Rotazione a Destra");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(rightTurnDuration);
            animator.SetBool("IsTurningRight", false);

           
        }
    }

    void Update()
    {
        // Puoi aggiungere eventuali aggiornamenti se necessari
    }
}








