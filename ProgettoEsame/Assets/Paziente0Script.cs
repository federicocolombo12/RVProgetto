using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Paziente0Script : MonoBehaviour
{
    public Transform thirdDestination; // Terza destinazione
    public Transform zeroDestination; // Destinazione zero
    public float walkSpeed = 1f; // Velocità di camminata
    public float runSpeed = 3f; // Velocità di corsa
    public float stoppingDistance = 0.5f; // Distanza di arresto
    public float idleTime = 2f; // Tempo in secondi prima che inizi a camminare

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isWaitingForPlayer = false;
    private bool interactions = false;

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
        StartCoroutine(PazienteRoutine());
    }

    private IEnumerator PazienteRoutine()
    {
        while (true)
        {
            // Stato iniziale: Idle
            Debug.Log("Inizio Idle");
            animator.SetBool("IsWalking", false);
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Passa allo stato di corsa verso la terza destinazione
            Debug.Log("Inizio Corsa verso la terza destinazione");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsRunning", true);
            navMeshAgent.speed = runSpeed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(thirdDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle
            Debug.Log("Arrivato alla terza destinazione");
            animator.SetBool("IsRunning", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("SetIdle", true);
            isWaitingForPlayer = true;

            // Attesa fino a quando il giocatore non preme il tasto E
            yield return new WaitUntil(() => interactions);

            // Imposta interactions a true per 1 secondo
            Debug.Log("Interazioni attive per 1 secondo");
            animator.SetBool("SetIdle", false);
            animator.SetBool("Interactions", true);
            yield return new WaitForSeconds(1f);
            interactions = false;

            // Passa allo stato di camminata verso la destinazione zero
            Debug.Log("Inizio Camminata verso la destinazione zero");
            animator.SetBool("Interactions", false);
            animator.SetBool("IsWalking", true);
            navMeshAgent.speed = walkSpeed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(zeroDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle
            Debug.Log("Arrivato alla destinazione zero");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);
        }
    }

    void Update()
    {
        // Puoi aggiungere eventuali aggiornamenti se necessari
    }

    public void Interact()
    {
        if (isWaitingForPlayer)
        {
            Debug.Log("Giocatore ha interagito con il Paziente 0");
            interactions = true;
            isWaitingForPlayer = false;
        }
    }
}
