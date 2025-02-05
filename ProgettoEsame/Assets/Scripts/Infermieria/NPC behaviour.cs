using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehavior : MonoBehaviour
{
    public Transform medicinePoint;
    public Transform exitPoint;
    public bool HasFinished { get; private set; } = false;

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private InfermieraScript nurse;
    private NpcFootstepAudio footstepAudio; // Per il suono dei passi

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<NpcFootstepAudio>();
        

        animator.SetBool("isWalking", false);
    }

    public void StartActions()
    {
        StartCoroutine(PerformActions());
    }

    private IEnumerator PerformActions()
    {

        // Cammina verso il punto della medicina
        StartWalking();
        agent.SetDestination(medicinePoint.position);
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.1f);

        // Ferma la camminata
        StopWalking();

        // Avvisa l'infermiera
        if (nurse != null)
        {
            nurse.TriggerNurseAnimation();
        }
        else
        {
            Debug.LogWarning("Nurse reference is missing");
        }

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("takeMedicine");
        yield return new WaitForSeconds(4f);

        // Cammina verso l'uscita
        StartWalking();
        HasFinished = true;
        agent.SetDestination(exitPoint.position);

        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);


        // Termina le azioni
        StopWalking();
        Destroy(gameObject);
    }

    private void StartWalking()
    {
        animator.SetBool("isWalking", true);
        footstepAudio?.StartFootsteps(); // Suono dei passi
    }

    private void StopWalking()
    {
        animator.SetBool("isWalking", false);
        footstepAudio?.StopFootsteps(); // Ferma i passi
    }
}
