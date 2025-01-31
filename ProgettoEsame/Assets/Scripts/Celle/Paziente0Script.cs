using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Paziente0Script : MonoBehaviour
{
    public static Paziente0Script instance { get; private set; }
    public Transform thirdDestination1; // Terza destinazione
    public Transform firstDestination1; // Terza destinazione
    public Transform zeroDestination; // Destinazione zero
    [SerializeField] float walkSpeed = 1f; // Velocità di camminata
    public float runSpeed = 3f; // Velocità di corsa
    public float stoppingDistance = 0.5f; // Distanza di arresto
    public float idleTime = 6f; // Tempo in secondi prima che inizi a camminare
    public GameObject knife; // Coltello dell'NPC
    public Camera playerCamera; // Camera del giocatore
    public float interactionDistance = 2f; // Distanza massima per l'interazione
    

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    //AudioManager
    public bool audiotalking = false;
    public bool audioRunning = false;
    public bool audioidle = false;
    public bool audiowalking = false;
    

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

    public IEnumerator PazienteRoutine()
    {
        yield return new WaitUntil(() => CellaManager.instance.attivaPazienteRoutine);

        while (true)
        {
            // Stato iniziale: Idle
            Debug.Log("Inizio Idle");
            animator.SetBool("IsWalking", false);
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Passa allo stato di corsa verso la prima destinazione
            Debug.Log("Inizio Corsa verso la prima destinazione");
            animator.SetBool("SetIdle", false);
            audioRunning = true;
            animator.SetBool("IsRunning", true);
            navMeshAgent.speed = runSpeed;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(firstDestination1.position);

            // Aspetta che l'NPC raggiunga la prima destinazione
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle e rotazione a destra
            Debug.Log("Arrivato alla prima destinazione, rotazione a destra");
            animator.SetBool("IsRunning", false);
            audioRunning = false;
            navMeshAgent.isStopped = true;
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(0.3f); // Durata della rotazione
            animator.SetBool("IsTurningRight", false);

            // Passa allo stato di corsa verso una destinazione intermedia
            Debug.Log("Inizio Corsa verso destinazione intermedia");
            audioRunning = true;
            animator.SetBool("IsRunning", true);
            navMeshAgent.isStopped = false;

            // Calcolo del punto intermedio
            Vector3 intermediatePoint = Vector3.Lerp(transform.position, thirdDestination1.position, 0.5f);
            navMeshAgent.SetDestination(intermediatePoint);

            // Aspetta che l'NPC raggiunga il punto intermedio
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Passa direttamente alla terza destinazione
            Debug.Log("Raggiunto punto intermedio, inizio corsa verso la terza destinazione");
            navMeshAgent.SetDestination(thirdDestination1.position);

            // Aspetta che l'NPC raggiunga la terza destinazione
            yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

            // Stato di idle
            Debug.Log("Arrivato alla terza destinazione");
            animator.SetBool("IsRunning", false);
            audioRunning = false;
            navMeshAgent.isStopped = true;
            animator.SetBool("SetIdle", true);
            

            // Attesa fino a quando il giocatore non preme il tasto E
            yield return new WaitUntil(() => NpcCelleInteractionManager.instance.paziente0Interaction);

            // Interazioni e comportamento successivo
            Debug.Log("Interazioni attive per 1 secondo");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsYelling", true);
            audiotalking = true;
            yield return new WaitForSeconds(7.6f);
            audiotalking = false;
            animator.SetBool("IsYelling", false);
            animator.SetBool("TakeThis", true);

            KnifePickUpandPlace knifeScript = knife.GetComponent<KnifePickUpandPlace>();
            knifeScript.ActivateKnifeTag(); ;

            // Attesa fino a quando il giocatore non prende il coltello
            yield return new WaitUntil(() => CellaManager.instance.coltelloPreso);

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
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(0.8f);
            animator.SetBool("SetIdle", true);

            break;
        }
    }


     
}
