using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CellGuardNpc : MonoBehaviour
{
    public float idleTime = 3f;
    public float secondIdleTime = 6f;
    public Transform firstDestination;
    public Transform secondDestination;
    public Transform thirdDestination;
    public float walkSpeed = 1f;
    public float stoppingDistance = 0.5f;
    public float rightTurnDuration = 1f;
    public float playerStoppingDistance = 1f;

    public static CellGuardNpc instance;

    private FirstPersonController player;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private AperturaPorta aperturaPorta;
    private CellGuardAudioManager guardAudioManager;

    public bool thirdPosition = false;

    public event System.Action OnAnimationEnd;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        guardAudioManager = GetComponent<CellGuardAudioManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<FirstPersonController>();
        aperturaPorta = FindObjectOfType<AperturaPorta>();

        if (animator == null) Debug.LogError("Animator non trovato!");
        if (navMeshAgent == null) Debug.LogError("NavMeshAgent non trovato!");
        if (guardAudioManager == null) Debug.LogError("GuardAudioManager non trovato!");

        navMeshAgent.speed = walkSpeed;
        navMeshAgent.stoppingDistance = stoppingDistance;

        StartCoroutine(GuardRoutine());
    }

    public IEnumerator GuardRoutine()
    {
        yield return new WaitUntil(() => CellaManager.instance.attivaGuardRoutine);

        while (true)
        {
            // Stato Idle
            SetAnimationState(idle: true);
            guardAudioManager.PlayIdle();
            yield return new WaitForSeconds(idleTime);

            // Camminata verso la prima destinazione
            MoveToDestination(firstDestination.position);
            guardAudioManager.PlayWalking();
            yield return WaitForNavMesh();

            // Idle + Rotazione a sinistra
            SetAnimationState(idle: true);
            guardAudioManager.PlayIdle();
            Rotate("IsTurningLeft");
            yield return new WaitForSeconds(secondIdleTime);

            // Rotazione a destra
            Rotate("IsTurningRight");

            // Camminata verso la seconda destinazione
            MoveToDestination(secondDestination.position);
            guardAudioManager.PlayWalking();
            yield return WaitForNavMesh();

            // Idle + Rotazione a sinistra
            SetAnimationState(idle: true);
            guardAudioManager.PlayIdle();
            Rotate("IsTurningLeft");

            // Aspetta che il coltello sia nascosto
            yield return new WaitUntil(() => CellaManager.instance.coltelloNascosto);

            // Rotazione a destra
            Rotate("IsTurningRight");

            // Camminata verso la terza destinazione
            MoveToDestination(thirdDestination.position);
            guardAudioManager.PlayWalking();
            yield return WaitForNavMesh();

            // Idle + Rotazione a sinistra
            SetAnimationState(idle: true);
            guardAudioManager.PlayIdle();
            Rotate("IsTurningLeft");

            thirdPosition = true;
            player.playerCanMove = false;
            if (thirdPosition && aperturaPorta != null) aperturaPorta.ApriPorta();

            yield return new WaitForSeconds(secondIdleTime);

            // Camminata verso il giocatore
            MoveToDestination(GetPlayerApproachPosition());
            guardAudioManager.PlayWalking();
            yield return WaitForNavMesh();

            // Idle davanti al giocatore
            SetAnimationState(idle: true);
            guardAudioManager.PlayIdle();
            RotateTowardsPlayer();
            yield return new WaitForSeconds(idleTime);

            // Segnala la fine dell'animazione
            OnAnimationEnd?.Invoke();
        }
    }

    private void SetAnimationState(bool idle = false, bool walking = false)
    {
        animator.SetBool("SetIdle", idle);
        animator.SetBool("IsWalking", walking);
    }

    private void MoveToDestination(Vector3 destination)
    {
        SetAnimationState(walking: true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination);
    }

    private IEnumerator WaitForNavMesh()
    {
        yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);
        navMeshAgent.isStopped = true;
        SetAnimationState(idle: true);
    }

    private void Rotate(string animationBool)
    {
        animator.SetBool(animationBool, true);
        StartCoroutine(ResetAnimationBool(animationBool, rightTurnDuration));
    }

    private IEnumerator ResetAnimationBool(string boolName, float duration)
    {
        yield return new WaitForSeconds(duration);
        animator.SetBool(boolName, false);
    }

    private Vector3 GetPlayerApproachPosition()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        return playerTransform.position - directionToPlayer * playerStoppingDistance;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

