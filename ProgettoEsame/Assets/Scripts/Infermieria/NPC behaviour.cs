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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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
        animator.SetBool("isWalking", true);
        agent.SetDestination(medicinePoint.position);

        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);

        // Prendi la medicina
        animator.SetBool("isWalking", false);
        animator.SetTrigger("takeMedicine");
        yield return new WaitForSeconds(2f); // Durata dell'animazione

        // Cammina verso il punto di uscita
        animator.SetBool("isWalking", true);
        agent.SetDestination(exitPoint.position);

        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);

        // Termina le azioni
        animator.SetBool("isWalking", false);
        HasFinished = true;
        Destroy(gameObject);
    }
}
