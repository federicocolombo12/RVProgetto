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
    public float walkSpeed = 1f; // Velocità di camminata
    public float stoppingDistance = 0.5f; // Distanza di arresto
    public float rightTurnDuration = 1f; // Durata della rotazione a destra
    public float playerStoppingDistance = 1f;
    public float blockDistance = 3f;
    public static CellGuardNpc instance;
    FirstPersonController player;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private AperturaPorta aperturaPorta;
    public bool OggettoNascosto = false;
    [SerializeField] public bool thirdPosition = false;

    // AudioManager: Stati audio
    [SerializeField] CellGuardAudioManager audioManager;

    // Evento che segnala la fine dell'animazione
    public event System.Action OnAnimationEnd;

    void Start()
    {
        audioManager = GetComponent<CellGuardAudioManager>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<FirstPersonController>();
        aperturaPorta = FindObjectOfType<AperturaPorta>();

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

    public IEnumerator GuardRoutine()
    {
        yield return new WaitUntil(() => CellaManager.instance.attivaGuardRoutine);

        Debug.Log("Coltello preso, inizio la routine del guardiano");

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
            audioManager.TalkingClip1();
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
            yield return new WaitUntil(() => CellaManager.instance.coltelloNascosto);

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

            thirdPosition = true;
            player.playerCanMove = false;

            if (thirdPosition && aperturaPorta != null)
            {
                aperturaPorta.ApriPorta();
            }

            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(secondIdleTime);

            // Camminata verso il giocatore
            Debug.Log("Inizio Camminata verso il giocatore");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Vector3 destination = playerTransform.position - directionToPlayer * playerStoppingDistance;
            navMeshAgent.SetDestination(destination);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Torna allo stato di idle
            Debug.Log("Arrivato alla quarta destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            RotateTowardsPlayer();
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Segnala la fine dell'animazione
            OnAnimationEnd?.Invoke();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Metodo per aggiornare lo stato audio
   
}