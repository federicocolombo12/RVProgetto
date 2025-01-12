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
        if (nurse != null)
        {
            Debug.Log("Notifying nurse");
            nurse.TriggerNurseAnimation();
        }
        else
        {
            Debug.LogWarning("Nurse reference is missing");
        }
        yield return new WaitForSeconds(1.5f); // Tempo per l'animazione dell'infermiera
        animator.SetTrigger("takeMedicine");

        // Notifica l'infermiera
        
        

        yield return new WaitForSeconds(4.567f); // Durata dell'animazione

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
