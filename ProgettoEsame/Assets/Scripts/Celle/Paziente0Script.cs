using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Paziente0Script : MonoBehaviour
{
    public static Paziente0Script instance { get; private set; }
    public Transform thirdDestination1;
    public Transform firstDestination1;
    public Transform zeroDestination;
    [SerializeField] float walkSpeed = 1f;
    public float runSpeed = 3f;
    public float stoppingDistance = 0.5f;
    public float idleTime = 6f;
    public GameObject knife;
    public Camera playerCamera;
    public float interactionDistance = 2f;
   

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private CellBackground cellBackground; // Riferimento allo script della musica
    private PortaIngersso portaIngersso;

    // AudioManager
    public bool audiotalking = false;
    public bool audioRunning = false;
    public bool audioidle = false;
    public bool audiowalking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        cellBackground = FindObjectOfType<CellBackground>(); // Trova lo script della musica
        portaIngersso = FindObjectOfType<PortaIngersso>();

        if (animator == null)
            Debug.LogError("Animator non trovato sul personaggio!");

        if (navMeshAgent == null)
            Debug.LogError("NavMeshAgent non trovato sul personaggio!");

        if (cellBackground == null)
            Debug.LogError("CellBackground non trovato nella scena!");

        navMeshAgent.speed = walkSpeed;
        navMeshAgent.stoppingDistance = stoppingDistance;

        StartCoroutine(PazienteRoutine());
    }

    public IEnumerator PazienteRoutine()
    {
        yield return new WaitUntil(() => CellaManager.instance.attivaPazienteRoutine);
        

        while (true)
        {
            portaIngersso.OpenDoor();
            // Stato Idle
            animator.SetBool("IsWalking", false);
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Corsa verso la prima destinazione
            animator.SetBool("SetIdle", false);
            audioRunning = true;
            animator.SetBool("IsRunning", true);
            navMeshAgent.speed = runSpeed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(firstDestination1.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Rotazione a destra
            animator.SetBool("IsRunning", false);
            portaIngersso.CloseDoor();
            audioRunning = false;
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(0.3f);
            animator.SetBool("IsTurningRight", false);

            // Corsa verso il punto intermedio
            audioRunning = true;
            animator.SetBool("IsRunning", true);
            navMeshAgent.isStopped = false;
            Vector3 intermediatePoint = Vector3.Lerp(transform.position, thirdDestination1.position, 0.5f);
            navMeshAgent.SetDestination(intermediatePoint);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Corsa verso la terza destinazione
            navMeshAgent.SetDestination(thirdDestination1.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato Idle
            animator.SetBool("IsRunning", false);
            audioRunning = false;
            navMeshAgent.isStopped = true;
            animator.SetBool("SetIdle", true);

            // Attesa interazione con il giocatore
            yield return new WaitUntil(() => NpcCelleInteractionManager.instance.paziente0Interaction);

            // Inizio dialogo
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsYelling", true);
            audiotalking = true;
            yield return new WaitForSeconds(7.6f);
            audiotalking = false;
            animator.SetBool("IsYelling", false);
            animator.SetBool("TakeThis", true);

            KnifePickUpandPlace knifeScript = knife.GetComponent<KnifePickUpandPlace>();
            knifeScript.ActivateKnifeTag();

            // Aspetta fino a quando il giocatore non prende il coltello
            yield return new WaitUntil(() => CellaManager.instance.coltelloPreso);

            // ?? Avvia la musica di sottofondo quando il coltello viene preso
            if (cellBackground != null)
            {
                cellBackground.PlaySuspenseMusic();
            }

            animator.SetBool("TakeThis", false);
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(1f);

            // Camminata verso la destinazione zero
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsWalking", true);
            navMeshAgent.speed = walkSpeed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(zeroDestination.position);
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(0.8f);
            animator.SetBool("SetIdle", true);

            break;
        }
    }
}

