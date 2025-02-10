using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CellGuardNpc : MonoBehaviour
{
    public float idleTime = 2f; 
    public float secondIdleTime = 5f; 
    public Transform firstDestination; 
    public Transform secondDestination; 
    public Transform thirdDestination; 
    public Transform fourthDestination; 
    public Transform fifthDestination; 
    public float walkSpeed = 1f; 
    public float stoppingDistance = 0.5f; 
    public float rightTurnDuration = 1f; 
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
    private PortaIngersso portaIngersso;

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
        portaIngersso = FindObjectOfType<PortaIngersso>();

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
            audioManager.TalkingClip0();
            yield return new WaitForSeconds(idleTime);
            

            // Passa allo stato di camminata verso la prima destinazione
            Debug.Log("Inizio Camminata verso la prima destinazione");
            animator.SetBool("SetIdle", false);
            portaIngersso.OpenDoor();
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(firstDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle e rotazione a sinistra
            Debug.Log("Arrivato alla prima destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            portaIngersso.CloseDoor();
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
            audioManager.TalkingClip2();
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
            audioManager.TalkingClip3();

            thirdPosition = true;

            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(secondIdleTime);

            // Rotazione a destra
            Debug.Log("Inizio Rotazione a Destra");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(rightTurnDuration);
            animator.SetBool("IsTurningRight", false);

            // Camminata verso la quarta destinazione
            Debug.Log("Inizio Camminata verso la quarta destinazione");
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(fourthDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle e rotazione a destra
            Debug.Log("Arrivato alla quarta destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("IsTurningRight", false);
            audioManager.TalkingClip4();

            // Camminata verso la quinta destinazione
            Debug.Log("Inizio Camminata verso la quinta destinazione");
            animator.SetBool("IsWalking", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(fifthDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle alla quinta destinazione
            Debug.Log("Arrivato alla quinta destinazione");
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            yield return RotateTowardsPlayer(transform, playerTransform.position);
            animator.SetBool("SetIdle", true);
            audioManager.TalkingClip5();
            yield return new WaitForSeconds(3f);


            // Segnala la fine dell'animazione
            OnAnimationEnd?.Invoke();
            yield return new WaitForSeconds(1f);
            break;
        }
    }

    private IEnumerator RotateTowardsPlayer(Transform npcTransform, Vector3 targetPosition)
    {
        Quaternion initialRotation = npcTransform.rotation;
        Vector3 direction = (targetPosition - npcTransform.position).normalized;
        direction.y = 0; // Mantieni la rotazione solo sull'asse Y
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float elapsedTime = 0f;
        float duration = 1f; // Durata della rotazione in secondi

        while (elapsedTime < duration)
        {
            npcTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        npcTransform.rotation = targetRotation;
    }

    // Metodo per aggiornare lo stato audio

}