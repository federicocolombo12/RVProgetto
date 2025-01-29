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
    [SerializeField] InfermieraScript nurse;
    private NpcFootstepAudio footstepAudio; // Aggiunto per il suono dei passi

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<NpcFootstepAudio>(); // Trova il componente audio

        // Assicurati che il personaggio sia in idle inizialmente
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

        // Prendi la medicina
        StopWalking();
        if (nurse != null)
        {
            Debug.Log("Notifying nurse");
            nurse.TriggerNurseAnimation();
        }
        else
        {
            Debug.LogWarning("Nurse reference is missing");
        }
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("takeMedicine");

        yield return new WaitForSeconds(4f);

        // Cammina verso il punto di uscita
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
        footstepAudio?.StartFootsteps(); // Avvia i passi se il componente esiste
    }

    private void StopWalking()
    {
        animator.SetBool("isWalking", false);
        footstepAudio?.StopFootsteps(); // Ferma i passi
    }
}
