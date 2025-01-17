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
    public GameObject knife; // Coltello dell'NPC
    public Camera playerCamera; // Camera del giocatore
    public float interactionDistance = 2f; // Distanza massima per l'interazione

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
            animator.SetBool("IsYelling", true);
            yield return new WaitForSeconds(5f);
            animator.SetBool("IsYelling", false);
            animator.SetBool("TakeThis", true);

            // Attesa fino a quando il giocatore non prende il coltello
            yield return new WaitUntil(() => KnifePickUpandPlace.coltelloPreso);

            animator.SetBool("TakeThis", false);
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(1f);

            // Passa allo stato di camminata verso la destinazione zero
            Debug.Log("Inizio Camminata verso la destinazione zero");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsWalking", true);
            navMeshAgent.speed = walkSpeed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(zeroDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle
            Debug.Log("Arrivato alla destinazione zero");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningRight", true); // Aggiunto IsTurningRight
            animator.SetBool("SetIdle", true);

            break;
        }
    }

    void Update()
    {
        if (isWaitingForPlayer && Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Giocatore ha interagito con il Paziente 0");
                    interactions = true;
                    isWaitingForPlayer = false;
                    CellaManager.instance.coltelloPreso = true;
                }
            }
        }
    }

    public void Interact()
    {
        if (isWaitingForPlayer)
        {
            Debug.Log("Giocatore ha interagito con il Paziente 0");
            interactions = true;
            isWaitingForPlayer = false;
            CellaManager.instance.coltelloPreso = true;
        }
    }
}
